﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Xml.Linq;

namespace ProjetCeption
{
    class Program
    {
        static void Main(string[] args)
        {
            //On crée le catalogue à partir des données du fichier Xml
            XmlSerializer xs = new XmlSerializer(typeof(Catalogue));
            StreamReader reader = new StreamReader("sauvegardeCatalogue.xml");
            Catalogue catalogueENSC = xs.Deserialize(reader) as Catalogue;
            reader.Close();


            AfficherMenu(catalogueENSC);



            static void AfficherMenu(Catalogue catalogue)
            {
                //Affichage du menu centré
                //Les éléments du menu à afficher sont stockés dans une liste. 
                //La liste est ensuite parcourue afin d'éviter les répétition de code pour centrer le texte
                string menu = "******************************* Menu *****************************";
                string m1 = "1-Rechercher un projet par intervenant";
                string m2 = "2-Rechercher un projet par année";
                string m3 = "3-Rechercher un projet par matière";
                string m4 = "4-Rechercher un projet par mot clé";
                string m5 = "5-Afficher tous les projets";
                string m6 = "6-Ajouter un projet";
                string m7 = "7-Supprimer un projet";
                string m8 = "8-Modifier un projet";
                string m9 = "9-Réinitialiser le catalogue";
                string m10 = "10-Quitter\n";
                string m11 = "Donner votre choix : ";
                Console.WriteLine("");

                string[] libelle = new string [] { menu, m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11 };

                for(int i=0;i<=11;i++)
                {
                    Console.SetCursorPosition((Console.WindowWidth - libelle[i].Length) / 2, Console.CursorTop);
                    Console.WriteLine(libelle[i]);
                }

                //Choix de l'utilisateur et appel à la fonction correspondante
                int choix = int.Parse(Console.In.ReadLine());
                switch (choix)
                {
                    case 1:
                        catalogue.RechercherParIntervenant(); 
                        break;

                    case 2:
                        catalogue.RechercherParAnnee();
                        break;

                    case 3:
                        catalogue.RechercherParMatiere();
                        break;

                    case 4:
                        catalogue.RechercherParMotCle();
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
                        catalogue.ModifierProjet();
                        break;

                    case 9:
                        catalogue.ReinitialiserCatalogue();
                        Console.WriteLine("Le catalogue a bien été réinitialisé !");
                        break;

                    case 10:
                        Environment.Exit(0);
                        break;


                }
                //Sauvegarde du catalogue après les éventuelles modifications
                catalogue.Sauvegarder("sauvegardeCatalogue.xml");

                //Affichage du menu
                AfficherMenu(catalogue);
            }            


        }
    }
}



