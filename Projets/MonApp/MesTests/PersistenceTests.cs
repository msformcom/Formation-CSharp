using Liste;
using MesClasses;
using MesClasses.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MesTests;

[TestClass]
public class PersistenceTests
{
    private readonly IServiceProvider injecteur ;
    public PersistenceTests()
    {
        #region Configuration injection de d�pendance
        // Objet contenant les d�pendances qui vont �tre propos�es par l'Injecteur

        ServiceCollection services = new ServiceCollection();

        #region Journalisation


        // Ajouter la journalisation vers les journaux �v�nement Windows
        // Apr�s installation de Microsoft.Extensions.Logging.EventLog
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

        // Association de configManager avec la d�pendance IConfiguration
        services.AddSingleton<IConfiguration>(configManager);
        #endregion


        // Package Microsoft.Extensions.Configuration.Binder
        // Ajouter un objet PersistenceToDiskOptions dont les valeurs sont enregistr�es dans la section
        // PersistenceToDiskOptions de la config

        PersistenceToDiskOptions persistenceToDiskOptions = configManager
                            .GetRequiredSection("PersistenceToDiskOptions")
                            .Get<PersistenceToDiskOptions>()!;
        services.AddSingleton(persistenceToDiskOptions);

        // Ajoute � mon injection la classe ListeCourses
        // Indique que je ne veux cr�er autant d'instance de l'objet que de demande
        services.AddTransient<ListeCourses>();


        // La classe de sauvegarde sera en mode singleton (1 seule instance suffit)
        // Attention aux d�pendances n�cessaires pour construire l'instance de PersistenceToDisk
        services.AddSingleton<IPersistenceListe<Guid>, PersistenceToDisk>();


        // Je cr�e un injecteur de d�pendance
        injecteur = services.BuildServiceProvider();
        #endregion


    }
    // Formation : Tester la sauvegarde
    // Comment Cr�er et configurer un injecteur de d�pendance
    [TestMethod]
    public async Task AddListAsyncTest()
    {
        // Que faire ?   

        // 1) Cr�er et configurer un injecteur de d�pendance
        // 2) Demander une instance de Liste
        // 3) Ajoute un item
        // 4) Cr�er un objet de type IPersistenceListe => Demander � l'Injection de D�pendance
        // 5) utiliser la m�thode AddListAsync de notre instance de IPersistenceListe






        // Demande de liste de course � l'injecteur
        var liste = injecteur.GetRequiredService<ListeCourses>();
        liste.Nom = "Mes courses";
        var item1 = new Item("Pates", 10M) { Description = "De jolies p�tes fraiches" };
        await liste.AddItemAsync(item1);

        // demande de la classe de sauvegarde
        var persistenceClass = injecteur.GetRequiredService<IPersistenceListe<Guid>>();

        var id= await persistenceClass.AddListeAsync(liste);

        var listeRestauree = await persistenceClass.GetListeCoursesAsync(id);

        Assert.AreEqual(liste.Nom, listeRestauree.Nom, "Le nom n'est pas sauvegard� correctement");
        Assert.AreEqual(liste.Items.Count(), listeRestauree.Items.Count());
        Assert.AreEqual(liste.Items.First().Libelle, listeRestauree.Items.First().Libelle);


    }
}
