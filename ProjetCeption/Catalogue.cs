



using System;
using System.Collections.Generic;
using System.Globalization;

namespace ProjetCeption
{
    public class Catalogue
    {
        public List<Matiere> ListeMatieres { get; set; }
        public List<Eleve> ListeEleves { get; set; }
        public List<Enseignant> ListeEnseignants { get; set; }
        public List<Externe> ListeExternes { get; set; }
        public List<Intervenant> ListeIntervenants { get; set; }


        public List<Livrable> ListeLivrables { get; set; }

        public List<Projet> ListeProjets { get; set; }

        public Catalogue()
        {
            Matiere ProgAv = new Matiere("Programmation avancée", "42");
            Matiere Gesp = new Matiere("GESP", "666");
            Matiere Signal = new Matiere("Signal", "465");
            ListeMatieres = new List<Matiere> { ProgAv, Gesp, Signal };

            Eleve Juliette = new Eleve("Esquirol", "Juliette", 2022);
            Eleve Léa = new Eleve("Grondin", "Léa", 2022);
            Eleve Hippo = new Eleve("Caubet", "Hyppolyte", 2021);
            ListeEleves = new List<Eleve> { Juliette, Léa, Hippo };

            List<Matiere> matieresPesquet = new List<Matiere> { ProgAv, Gesp };
            Enseignant Pesquet = new Enseignant("Pesquet", "Baptiste", matieresPesquet);
            List<Matiere> matieresLanusse = new List<Matiere> { Signal };
            Enseignant Lanusse = new Enseignant("Lanusse", "Patrick", matieresLanusse);
            ListeEnseignants = new List<Enseignant> { Pesquet, Lanusse };

            Externe Milo = new Externe("Toumine", "Milo", "Cobaye BCI");
            ListeExternes = new List<Externe> { Milo };

            ListeIntervenants = new List<Intervenant> { };
            foreach (Intervenant i in ListeEleves)
                ListeIntervenants.Add(i);
            foreach (Intervenant i in ListeEnseignants)
                ListeIntervenants.Add(i);
            foreach (Intervenant i in ListeExternes)
                ListeIntervenants.Add(i);

            Livrable siteWeb = new Livrable("Site web");
            Livrable analyseExistant = new Livrable("Analyse de l'existant");
            Livrable rapport = new Livrable("Rapport");
            ListeLivrables = new List<Livrable> { siteWeb, analyseExistant, rapport };

            // Est-ce que on ne peut pas tout simplement créer un role comme ça : Role Tuteur = Role()  ?
            //Mais dans ce cas qu'est ce qu'il y a dans la classe role ?
            Role Tuteur = new Role("tuteur");
            Role Cobaye = new Role("cobaye");
            Role Acteur = new Role("acteur");

            List<Livrable> livrablesProjet1 = new List<Livrable> { siteWeb, rapport };
            List<Matiere> matieresProjet1 = new List<Matiere> { ProgAv, Gesp };
            List<Intervenant> intervenantsProjet1 = new List<Intervenant> { Juliette, Milo, Pesquet };
            Projet Projet1 = new Projet("Transdi", "Projet 1", true, "01/08/2015", "01/09/2015", intervenantsProjet1.Count, intervenantsProjet1, matieresProjet1, livrablesProjet1);
            //Si on fait comme ça, qu'est ce qu'on met dans AssocierRole ??
            Projet1.AssocierRoleIntervenant(Acteur, Juliette);
            Projet1.AssocierRoleIntervenant(Cobaye, Milo);
            Projet1.AssocierRoleIntervenant(Tuteur, Pesquet);



            List<Livrable> livrablesProjet2 = new List<Livrable> { analyseExistant, rapport };
            List<Matiere> matieresProjet2 = new List<Matiere> { Signal };
            List<Intervenant> intervenantsProjet2 = new List<Intervenant> { Léa, Hippo, Lanusse };
            Projet Projet2 = new Projet("Transpromo", "Projet 2", true, "01/09/2019", "30/05/2020", intervenantsProjet2.Count, intervenantsProjet2, matieresProjet2, livrablesProjet2);

            ListeProjets = new List<Projet> { Projet1, Projet2 };

        }
        public List<Projet> RechercherParIntervenant(string nomRecherche, string prenomRecherche)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                foreach (Intervenant intervenant in projet.IntervenantsConcernes)
                {
                    if (intervenant.Nom == nomRecherche || intervenant.Prenom == prenomRecherche)
                    {
                        Resultat.Add(projet);
                    }
                }
            }
            return Resultat;
        }

        public List<Projet> RechercherParAnnee(int anneeRecherchee)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                if (projet.DateDebut.Year == anneeRecherchee || projet.DateFin.Year == anneeRecherchee)
                {
                    Resultat.Add(projet);
                }
            }
            return Resultat;
        }

        public List<Projet> RechercherParMatiere(string matiereRecherchee)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                foreach (Matiere matiere in projet.MatieresConcernees)
                {
                    if (matiere.NomMatiere == matiereRecherchee )
                    {
                        Resultat.Add(projet);
                    }
                }
            }
            return Resultat;
        }



        public void AjouterProjet()
        {
            Console.WriteLine("\nVeuillez rentrer toutes les informations nécessaires :");
            Console.Write("\nType de projet : ");
            string nvType = Console.ReadLine();
            Console.Write("Thème : ");
            string nvTheme = Console.ReadLine();

            bool nvSujetLibre = true;
            Console.Write("Sujet libre (O/N) : ");
            string rep = Console.ReadLine();

            while (rep !="O" && rep != "N")
            {
                Console.WriteLine("Je n'ai pas compris votre réponse.");
                Console.Write("Sujet libre (O/N) : ");
                rep = Console.ReadLine();
            }
            if (rep == "O")
                nvSujetLibre = true;
            else 
                nvSujetLibre = false;

            //Pose prb à la création du projet si la date rentrée n'est pas valide
            Console.Write("Date de début (jj/mm/aaaa) : ");
            string nvDebut = Console.ReadLine();
            Console.Write("Date de fin (jj/mm/aaaa) : ");
            string nvFin = Console.ReadLine();

            //Création de la liste d'intervenants associée au projet
            //On affiche la liste des intervenants existants
            //L'utilisateur slectione les intervenants qu'il souhaite, quand il a fini il tape 0 pour sortir de la boucle
            Console.WriteLine("\n\n----- Intervenants-----");
            List<Intervenant> nvListeIntervenant = new List<Intervenant> { };
            Console.WriteLine("Voici la liste des intervenants possible : ");
            int j = 1;
            foreach (Intervenant i in ListeIntervenants)
            {
                Console.WriteLine("{0} - {1}", j, i.ToString());
                j++;
            }
            int choixIntervenant = 1;
            while (choixIntervenant != 0)
            {
                Console.Write("Ajouter un intervenant (entrez 0 pour finir) : ");
                choixIntervenant = Convert.ToInt32(Console.ReadLine());
                if(choixIntervenant !=0)
                {
                    nvListeIntervenant.Add(ListeIntervenants[choixIntervenant - 1]);
                    Console.WriteLine("\tL'intervenant a bien été ajouté");
                }
            }

            //Exactement la même chose pour les matières
            Console.WriteLine("\n\n----- Matières concernées-----");
            List<Matiere> nvListeMatieres = new List<Matiere> { };
            Console.WriteLine("Voici la liste des matières possible : ");
            j = 1;
            foreach (Matiere m in ListeMatieres)
            {
                Console.WriteLine("{0} - {1}", j, m.ToString());
                j++;
            }
            int choixMatiere = 1;
            while (choixMatiere != 0)
            {
                Console.Write("Ajouter une matière (entrez 0 pour finir) : ");
                choixMatiere = Convert.ToInt32(Console.ReadLine());
                if (choixMatiere != 0)
                {
                    nvListeMatieres.Add(ListeMatieres[choixMatiere - 1]);
                    Console.WriteLine("\tLa matière a bien été ajoutée");
                }
                    
            }

            //Exactement la même chose pour les livrables
            Console.WriteLine("\n\n----- Livrables-----");
            List<Livrable> nvListeLivrable = new List<Livrable> { };
            Console.WriteLine("Voici la liste des livrables possible : ");
            j = 1;
            foreach (Livrable l in ListeLivrables)
            {
                Console.WriteLine("{0} - {1}", j, l.ToString());
                j++;
            }
            int choixLivrable = 1;
            while (choixLivrable != 0)
            {
                Console.Write("Ajouter un livrable (entrez 0 pour finir) : ");
                choixLivrable = Convert.ToInt32(Console.ReadLine());
                if (choixLivrable != 0)
                {
                    nvListeLivrable.Add(ListeLivrables[choixLivrable - 1]);
                    Console.WriteLine("\tLe livrable a bien été ajouté");
                }
                    
            }

            Projet nouveauProjet = new Projet(nvType, nvTheme, nvSujetLibre, nvDebut, nvFin, nvListeIntervenant.Count, nvListeIntervenant, nvListeMatieres, nvListeLivrable);
            ListeProjets.Add(nouveauProjet);
            Console.WriteLine("\nLe projet a bien été ajouté !");
        }



        public void SupprimerProjet()
        {
            Console.WriteLine("Voici la liste des projets : ");
            int j = 1;
            foreach (Projet projet in ListeProjets)
            {
                Console.WriteLine("{0} - {1}\n", j, projet.ToString());
                j++;
            }

            Console.Write("\nLequel voulez-vous supprimer ?  ");
            int choixSupression = Convert.ToInt32(Console.ReadLine());
            ListeProjets.RemoveAt(choixSupression - 1);

            Console.WriteLine("Le projet a bien été supprimé !");

        }

    }
}



