using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    class Program
    {
        static void Main(string[] args)
        {
            //Catalogue projetsENSC = new Catalogue();

            //Test si Eleve fonctionne bien --> OK
            Eleve Moi = new Eleve("Esquirol", "Juliette", 2022, 2020);
            Console.WriteLine(Moi.ToString());

            //Test si Enseignant fonctionne bien --> OK
            Matiere ProgAv = new Matiere("programmation avancée", "42");
            Matiere Gesp = new Matiere("GESP", "666");
            List<Matiere> matieresPesquet = new List<Matiere>();
            matieresPesquet.Add(ProgAv);
            matieresPesquet.Add(Gesp);
            Enseignant ProfInfo = new Enseignant("Pesquet", "Baptiste", matieresPesquet);
            Console.WriteLine(ProfInfo.ToString());

            //Test si Exterieur fonctionne bien --> OK
            Exterieur Milo = new Exterieur("Toumine", "Milo", "Cobaye BCI");
            Console.WriteLine(Milo.ToString());

            //Test si la vérif de la matière fonctionne bien --> OK
            Matiere Apero = new Matiere("boire l'apéro", "mdr");

            //Test si Projet fonctionne bien
            List<Intervenant> intervenantsTransdiBCI = new List<Intervenant>();
            intervenantsTransdiBCI.Add(Moi);
            intervenantsTransdiBCI.Add(Milo);

            Livrable siteWeb = new Livrable("site web");
            Livrable analyseExistant = new Livrable("analyse de l'existant");
            List<Livrable> livrablesTransdiBCI = new List<Livrable>();
            livrablesTransdiBCI.Add(siteWeb);
            livrablesTransdiBCI.Add(analyseExistant);

            Projet Transdi = new Projet(6, false, "01/10/2019", "01/05/2020", "Projet transdisciplinaire sur le BCI", livrablesTransdiBCI, matieresPesquet, intervenantsTransdiBCI);

            Console.WriteLine(Transdi.ToString());

            Console.ReadKey();



        }
    }
}
