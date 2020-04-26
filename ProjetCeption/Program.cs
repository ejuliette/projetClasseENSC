﻿using System;
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
                //Affichage du menu centré
                int choix;
                string menu = "******************************* Menu *****************************";
                string m1 = "1-Rechercher un projet par intervenant";
                string m2 = "2-Rechercher un projet par année";
                string m3 = "3-Rechercher un projet par matière";
                string m4 = "4-Rechercher un projet par mot clé";
                string m5 = "5-Afficher tous les projets";
                string m6 = "6-Ajouter un projet";
                string m7 = "7-Supprimer un projet";
                string m8 = "8-Quitter\n";
                string m9 = "Donner votre choix : ";

                Console.WriteLine("");
                Console.SetCursorPosition((Console.WindowWidth - menu.Length) / 2, Console.CursorTop);
                Console.WriteLine(menu);
                Console.SetCursorPosition((Console.WindowWidth - m1.Length) / 2, Console.CursorTop);
                Console.WriteLine(m1);
                Console.SetCursorPosition((Console.WindowWidth - m2.Length) / 2, Console.CursorTop);
                Console.WriteLine(m2);
                Console.SetCursorPosition((Console.WindowWidth - m3.Length) / 2, Console.CursorTop);
                Console.WriteLine(m3);
                Console.SetCursorPosition((Console.WindowWidth - m4.Length) / 2, Console.CursorTop);
                Console.WriteLine(m4);
                Console.SetCursorPosition((Console.WindowWidth - m5.Length) / 2, Console.CursorTop);
                Console.WriteLine(m5);
                Console.SetCursorPosition((Console.WindowWidth - m6.Length) / 2, Console.CursorTop);
                Console.WriteLine(m6);
                Console.SetCursorPosition((Console.WindowWidth - m7.Length) / 2, Console.CursorTop);
                Console.WriteLine(m7);
                Console.SetCursorPosition((Console.WindowWidth - m8.Length) / 2, Console.CursorTop);
                Console.WriteLine(m8);
                Console.SetCursorPosition((Console.WindowWidth - m9.Length) / 2, Console.CursorTop);
                Console.Write(m9);

                choix = int.Parse(Console.In.ReadLine());
                switch (choix)
                {
                    case 1:
                        Console.Write("\nNom : ");
                        string nom = Console.ReadLine();
                        Console.Write("Prénom : ");
                        string prenom = Console.ReadLine();
                        if (catalogue.RechercherParIntervenant(nom, prenom).Count != 0)
                        {
                            foreach (Projet projet in catalogue.RechercherParIntervenant(nom, prenom))
                                Console.WriteLine(projet.ToString());
                        }
                        else
                            Console.WriteLine("Aucun résultat ne correspond à votre recherche");
                        break;


                    case 2:
                        Console.Write("\nAnnée : ");

                        int annee = Convert.ToInt32(Console.ReadLine());
                        if (catalogue.RechercherParAnnee(annee).Count != 0)
                        {
                            foreach (Projet projet in catalogue.RechercherParAnnee(annee))
                                Console.WriteLine(projet.ToString());
                        }
                        else
                            Console.WriteLine("Aucun résultat ne correspond à votre recherche");
                        break;
                        

                    case 3:
                        //On présente la liste des matières possible
                        Console.WriteLine("Voici la liste des matières possible : ");
                        int j = 1;
                        foreach (Matiere m in catalogue.ListeMatieres)
                        {
                            Console.WriteLine("{0} - {1}", j, m.ToString());
                            j++;
                        }
                        //l'utilisateur en choisi une parmi celles proposées (on vérifie qu'il a entré un numéro valide)
                        Console.Write("Rechercher la matière : ");
                        int numMatiere = Convert.ToInt32(Console.ReadLine());
                        while(numMatiere <1 || numMatiere > j-1)
                        {
                            Console.WriteLine("Je n'ai pas compris votre choix");
                            Console.Write("Rechercher la matière : ");
                            numMatiere = Convert.ToInt32(Console.ReadLine());
                        }
                        //On attribue le numéro entré par l'utilisateur à une matière
                        Matiere matiere = catalogue.ListeMatieres[numMatiere - 1];

                        //On vérifie que le résultat de la recherche n'est pas vide
                        if (catalogue.RechercherParMatiere(matiere).Count != 0)
                        {
                            foreach (Projet projet in catalogue.RechercherParMatiere(matiere))
                                Console.WriteLine(projet.ToString());
                        }
                        else
                            Console.WriteLine("Aucun résultat ne correspond à votre recherche");
                        
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



