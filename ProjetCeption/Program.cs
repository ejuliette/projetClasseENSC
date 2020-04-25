using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetCeption
{
    class Program
    {
        static void Main(string[] args)
        {
            Catalogue catalogueENSC = new Catalogue();
            AfficherMenu(catalogueENSC);


            static void AfficherMenu(Catalogue catalogue)
            {
                int choix;

                Console.WriteLine("\n*******************************Menu*****************************");
                Console.WriteLine("1-Rechercher un projet par intervenant");
                Console.WriteLine("2-Rechercher un projet par année");
                Console.WriteLine("3-Rechercher un projet par matière");
                Console.WriteLine("4-Rechercher un projet par mot clé");
                Console.WriteLine("5-Afficher tous les projets");
                Console.WriteLine("6-Ajouter un projet");
                Console.WriteLine("7-Supprimer un projet");
                Console.WriteLine("8-Quitter");
                Console.Write("Donner votre choix: ");
                choix = int.Parse(Console.In.ReadLine());
                switch (choix)
                {
                    case 1:
                        Console.Write("\nNom : ");
                        string nom = Console.ReadLine();
                        Console.Write("Prénom : ");
                        string prenom = Console.ReadLine();
                        foreach (Projet projet in catalogue.RechercherParIntervenant(nom, prenom))
                            Console.WriteLine(projet.ToString());
                        break;
                    case 2:
                        Console.Write("\nAnnée : ");
                        int annee = Convert.ToInt32(Console.ReadLine());
                        foreach (Projet projet in catalogue.RechercherParAnnee(annee))
                            Console.WriteLine(projet.ToString());
                        break;

                    case 3:
                        Console.Write("\nMatière : ");
                        string matiere = Console.ReadLine();
                        foreach (Projet projet in catalogue.RechercherParMatiere(matiere))
                            Console.WriteLine(projet.ToString());
                        break;

                    case 5:
                        foreach (Projet projet in catalogue.ListeProjets)
                            Console.WriteLine(projet.ToString());
                        break;

                    case 6:
                        catalogue.AjouterProjet();
                        break;

                    case 7:
                        catalogue.SupprimerProjet();
                        break;

                    case 8:
                        Environment.Exit(0);
                        break;
                }
                AfficherMenu(catalogue);
            }            


        }
    }
}



