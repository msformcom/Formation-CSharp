using Liste;
using MesClasses;
using MesClasses.Persistence;
using MesClasses.Persistence.BDD;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MesTests;

[TestClass]
public class PersistenceTests
{
    private readonly IServiceProvider injecteur ;
    public PersistenceTests()
    {
        #region Configuration injection de dépendance
        // Objet contenant les dépendances qui vont être proposées par l'Injecteur

        ServiceCollection services = new ServiceCollection();

        #region Journalisation


        // Ajouter la journalisation vers les journaux évènement Windows
        // Après installation de Microsoft.Extensions.Logging.EventLog
        services.AddLogging(l => l.AddEventLog());

        #endregion
        #region Configuration
        // Package Microsoft.Extensions.Configuration
        ConfigurationManager configManager = new ConfigurationManager();


        // Package : Microsoft.Extensions.Configuration.Json
        // AddJsonFile permet a configManager de lire un fichier json
        configManager.AddJsonFile("MaConfig.json");



        //#if DEBUG
        //        configManager.AddJsonFile("MaConfig.debug.json");
        //#endif
        // Package : Microsoft.Extensions.Configuration.Xml
        // configManager.AddXmlFile("MaConfig.xml");

        // Association de configManager avec la dépendance IConfiguration
        services.AddSingleton<IConfiguration>(configManager);
        #endregion

        #region Config Disque
        // Package Microsoft.Extensions.Configuration.Binder
        // Ajouter un objet PersistenceToDiskOptions dont les valeurs sont enregistrées dans la section
        // PersistenceToDiskOptions de la config

        PersistenceToDiskOptions persistenceToDiskOptions = configManager
                            .GetRequiredSection("PersistenceToDiskOptions")
                            .Get<PersistenceToDiskOptions>()!;
        services.AddSingleton(persistenceToDiskOptions);

        #endregion


        // La classe de sauvegarde sera en mode singleton (1 seule instance suffit)
        // Attention aux dépendances nécessaires pour construire l'instance de PersistenceToDisk

        // Recherche du nom complet de la classe utilisée pour IPersistenceListe dans la config
        //var persistenceClass = configManager.GetSection("PersistenceClasse").Value;
        // Recherche du Type 
        //var persistenceClassType = Type.GetType(persistenceClass);
        // Association IPersistenceListe => persistenceClassType
 //       services.AddSingleton(typeof(IPersistenceListe<Guid>), persistenceClassType));

        #region Config BDD
        services.AddDbContext<ListeDbContext>(options =>
        {
            // Cette fonction est utilisée pour créer les options
            // Ajouter le package du provider => Microsoft.EntityFrameworkCore.SqlServer
            // Sans config, le serveur installé avec Visual Studio est untilise
            options.UseSqlServer("name=MaConnection");
        });




        services.AddSingleton<IPersistenceListe<Guid>, PersistenceToBDD>();
        #endregion

        // Ajoute à mon injection la classe ListeCourses
        // Indique que je ne veux créer autant d'instance de l'objet que de demande
        services.AddTransient<ListeCourses>();




        // Je crée un injecteur de dépendance
        injecteur = services.BuildServiceProvider();
        #endregion


    }

    [TestMethod]
    public void CreationBDDTest()
    {
        var context = injecteur.GetService<ListeDbContext>();
        context.Database.EnsureCreated();

    }






    // Formation : Tester la sauvegarde
    // Comment Créer et configurer un injecteur de dépendance
    [TestMethod]
    public async Task AddListAsyncTest()
    {
       


        // Que faire ?   

        // 1) Créer et configurer un injecteur de dépendance
        // 2) Demander une instance de Liste
        // 3) Ajoute un item
        // 4) Créer un objet de type IPersistenceListe => Demander à l'Injection de Dépendance
        // 5) utiliser la méthode AddListAsync de notre instance de IPersistenceListe






        // Demande de liste de course à l'injecteur
        var liste = injecteur.GetRequiredService<ListeCourses>();
        liste.Nom = "Mes courses";
        var item1 = new Item("Pates", 10M) { Description = "De jolies pâtes fraiches" };
        await liste.AddItemAsync(item1);

        // demande de la classe de sauvegarde
        var persistenceClass = injecteur.GetRequiredService<IPersistenceListe<Guid>>();

        var id= await persistenceClass.AddListeAsync(liste);

        var listeRestauree = await persistenceClass.GetListeCoursesAsync(id);

        Assert.AreEqual(liste.Nom, listeRestauree.Nom, "Le nom n'est pas sauvegardé correctement");
        Assert.AreEqual(liste.Items.Count(), listeRestauree.Items.Count());
        Assert.AreEqual(liste.Items.First().Libelle, listeRestauree.Items.First().Libelle);


    }
}
