using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    class Program
    {
        static void Main(string[] args)
        {
            Catalogue catalogueENSC = new Catalogue();
            Recherche recherche = new Recherche();
            recherche.RechercherParAnnee(catalogueENSC, 2020);

            //recherche.RechercherParAnnee(2020);



            /*
            int choix;
            Catalogue<Projet> ResultatsRechercheNom;
            string nomEleve;

            Console.Out.WriteLine("*******************************Menu*****************************");
            Console.Out.WriteLine("1-Rechercher un projet par nom d'élève");
            Console.Out.WriteLine("2-Ajouter un projet");
            Console.Out.WriteLine("3-Modifier un projet");
            Console.Out.WriteLine("4-Supprimer un projet");
            Console.Out.WriteLine("5-Rechercher un projet par nom d'enseignant");
            Console.Out.WriteLine("6-Rechercher un projet par mot clé");
            Console.Out.WriteLine("7-Afficher tous les projets");
            Console.Out.WriteLine("8-Quitter");
            Console.Out.Write("Donner votre choix: ");
            choix = int.Parse(Console.In.ReadLine());
            switch (choix)
            {
                case 1:
                    Console.Out.Write("Donner le nom de l'élève: ");
                    nomEleve = Console.In.ReadLine();
                    ResultatsRechercheNom = Rechercher(ListeProjets, nomEleve);
                    if (ResultatsRechercheNom[0] == null)
                    {
                        Console.Out.WriteLine("Le projet est introuvable");
                    }
                    else
                    {
                        for (int i = 0; i < 10; i++) //10 pcq je sais pas cb il y a de résultats qui macthent
                            Console.Out.WriteLine(ResultatsRechercheNom[i]);
                    }
                    break;
            }






            Console.ReadKey();
            */


        }
    }
}



