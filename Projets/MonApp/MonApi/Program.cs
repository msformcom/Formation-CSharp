
using AutoMapper;
using MesClasses.Persistence.BDD;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonApi.DTO;
using System;
using System.Diagnostics;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);
// builder sert à configurer notre api

// Ajout du DbContextDont j'ai besoin
builder.Services.AddDbContext<ListeDbContext>(options =>
{
    // Configuration du DbContext : Provider + chaine de connexion
    options.UseSqlServer("name=MaConnection");
    // Permet de charger automatiquement les propriétés de navigation
    // Au moment où elles sont lues
     options.UseLazyLoadingProxies(false);
});
// ListeDbContext utilise ModelBuilderDelegate pour les spécificités de la BDD
builder.Services.AddSingleton<ModelBuilderDelegate>(modelBuilder =>
{
    // Personnalisation relatives à ListeDAO
    modelBuilder.Entity<ListeDAO>(listDAO =>
    {
        listDAO.ToTable("TBL_Listes");
        //...
    });
    modelBuilder.Entity<ItemDAO>(itemDAO =>
    {
        itemDAO.ToTable("TBL_Items");

        // Spécifier la clé primaire de la table associée à ItemDAO
        itemDAO.HasKey(c => c.Id);
        itemDAO.Property(c => c.Id).HasColumnName("PK_Item");
        // spécifier la longueur max de la colonne associée à Label
        itemDAO.Property(c => c.Label).HasMaxLength(50);

        // Optimise la recherche des items par Label
        itemDAO.HasIndex(c => c.Label);

        itemDAO.HasOne(c => c.Liste).WithMany(c => c.Items).HasForeignKey(c => c.FK_Liste);
    });
});
// Ajout dans DI d'un mapper DTO <=> DAO
builder.Services.AddAutoMapper(config =>
{
    // Le mapper est configuré pour mapper automatiquement les propriété de même nom
    config.CreateMap<ListeDAO, ListeDTO>()
        .ForMember(c => c.Libelle, o => { o.MapFrom(c => c.Name); })
        .ForMember(c => c.Items, o => {
            o.Condition(c => c.Items != null);
        
        })
        .ReverseMap();
    config.CreateMap<ItemDAO, ItemDTO>()
    .ForMember(c => c.Libelle, o => { o.MapFrom(c => c.Label); })
    .ForMember(c => c.Prix, o => { o.MapFrom(c => c.Price); })
    .ForMember(c => c.Points, o => { o.MapFrom(c => c.Tickets); })
    .ForMember(c => c.DateRealisation, o => { o.MapFrom(c => c.RealisationDate); })
    .ReverseMap();
});


var app = builder.Build();



// Configure the HTTP request pipeline.
//app.Use(
// //Premier traitement d'une requete entrente
// ////context => Contient toutes les infos sur la requete et sur la réponse
// //next => fonction qui représente les traitement des middle suivants

// async (HttpContext context, Func<Task> next) =>
//{
//    var url = context.Request.Path;
//    // Actions sur la requete

//    // Passer la mains aux middleware qui suivent
//    await next();

//    // Les middlewares suivants ont fait leur boulot
//    await context.Response.WriteAsync("<!-- Copyright 2025 Moi -->");
//});

app.Use(async (HttpContext context, Func<Task> next) =>
{
    Debug.WriteLine("{0:hh:mm:ss} Entrée : {1}", DateTime.Now, context.Request.Path);
    await next();
    Debug.WriteLine("{0:hh:mm:ss} Sortie : {1}", DateTime.Now, context.Request.Path);
    //await context.Response.WriteAsync("Coucou, " + context.Connection.RemoteIpAddress.ToString());
});

// Les associations url => Fonctions sont placés en derneir
app.MapGet("/Listes",
        // Cette fonction gère la requète vers l'url /Liste
        async (
        // Demande du HttpContext
        HttpContext httpContext,
        // Demande un ListeDbContext
        ListeDbContext dbContext) =>
        {
            var listes = dbContext.Listes;
            // Renvoi de DAO => Très très très très dangereux
            await httpContext.Response.WriteAsJsonAsync(listes);
        });

app.MapGet("/api/Listes/Count", async (ListeDbContext dbContext) =>
{
    // Le retour de la fonction est sérialisé automatiquement en JSON
    return await dbContext.Listes.CountAsync();
});
app.MapGet("/api/Listes", (
    ListeDbContext dbContext, // Accès aux données
    IMapper mapper // Mapping DTO <=> DAO
    ) =>
{


    // Attention
    var listes = dbContext.Listes;
    // SELECT * FROM TBL_Listes
    foreach (var liste in listes)
    {
        var itemList = liste.Items;
        // SELECT * FROM Items WHERE FK_Liste=7868226 => Executé pour chaque liste 
        // Dans le cas où on sait à l'avance qu'on va utiliser les Items de la Liste
        // Include(c=>c.Items)
    }



    // Le retour de la fonction est sérialisé automatiquement en JSON
    return dbContext.Listes.Select(c => mapper.Map<ListeDTO>(c));
});
// Get : /api/Liste/70ce92be-281c-4244-8504-2cc0c466b43d?withItems=true
app.MapGet("/api/Liste/{id:guid}", async (
        
        [FromServices] ListeDbContext dbContext, // dbContext à partir des services
        [FromRoute] Guid id, // => // id à partir de l'url,
        [FromServices] IMapper mapper,
         [FromQuery] bool withItems = false) =>
{
    // Le retour de la fonction est sérialisé automatiquement en JSON

    // Eager Loading => On charge les données en avance 
    //var daoEager = dbContext.Listes.Include(c => c.Items).First(c => c.Id == id);
    // daoEager.Items => Rempli

    var daoExplicit = dbContext.Listes.Find(id);
    // daoExplicit.Items => null
    if (withItems)
    {
        // Chargement explicite => quand / si je me veux
        await dbContext.Entry(daoExplicit!).Collection(c => c.Items).LoadAsync();
        // daoExplicit.Items => rempli
    }


    // Lasy => Le chargement sera fait automatiquement en cas de lecture des items
    // Ajout des proxies en tant que package
    var daoLasy = dbContext.Listes.Find(id);
    // Requète envoyée sans jointure pour Items
    var items = daoLasy.Items;
    // Requète envoyée pour obtenir les items de la liste
    return mapper.Map<ListeDTO>(daoExplicit);

});

app.MapPost("api/Listes", (
    [FromBody]ListeDTO liste,
    [FromServices] ListeDbContext dbContext,
    [FromServices] IMapper mapper
    ) => {
        if (liste.Id == null)
        {
            liste.Id= Guid.NewGuid();   
        }
        var dao = mapper.Map<ListeDAO>(liste);
        
        // Service : Vérifie que les données du dao sont correctes
        dbContext.Add(dao);

        dbContext.SaveChangesAsync();
        return dao.Id;

});

// Lance le server
app.Run();
