using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ProjetCeption
{
    public class Catalogue
    {
        public List<Eleve> ListeEleves { get; set; }
        public List<Enseignant> ListeEnseignants { get; set; }
        public List<Externe> ListeExternes { get; set; }
        public List<Intervenant> ListeIntervenants { get; set; }
        public List<Role> ListeRoles { get; set; }
        public List<Matiere> ListeMatieres { get; set; }
        public List<Livrable> ListeLivrables { get; set; }
        public List<Projet> ListeProjets { get; set; }
        public List<String> ListeTypeProjet { get; set; }

        public Catalogue()
        {
            /*
            string transdi = "transdisciplinaire";
            string transpromo = "transpromo";
            string PFE = "PFE";
            string progAvancee = "programmation avancée";
            string RAO = "RAO";

            ListeTypeProjet = new List<string> { transdi, transpromo, PFE, progAvancee, RAO };
            */
        }

        public List<Projet> RechercherParIntervenant(string nomRecherche, string prenomRecherche)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                foreach (Intervenant intervenant in projet.IntervenantsConcernes)
                {
                    if (intervenant.Nom == nomRecherche && intervenant.Prenom == prenomRecherche)
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

        public List<Projet> RechercherParMatiere(Matiere matiereRecherchee)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                foreach (Matiere matiere in projet.MatieresConcernees)
                {
                    if (matiere.NomMatiere == matiereRecherchee.NomMatiere )
                    {
                        Resultat.Add(projet);
                    }
                }
            }
            return Resultat;
        }

        public List<Projet> RechercherParMotCle(string motcle)
        {
            //Ne fais pas la recherche sur les années
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                if (projet.TypeProjet.IndexOf(motcle) != -1)
                {
                    Resultat.Add(projet);
                }

                if (projet.Theme.IndexOf(motcle) != -1)
                {
                    Resultat.Add(projet);
                }

                foreach (Matiere matiere in projet.MatieresConcernees)
                {
                    if (matiere.NomMatiere.IndexOf(motcle) != -1 || matiere.CodeSyllabus.IndexOf(motcle) !=-1)
                    {
                        Resultat.Add(projet);
                    }
                }
                foreach (Intervenant intervenant in projet.IntervenantsConcernes)
                {
                    if (intervenant.Nom.IndexOf(motcle) != -1 || intervenant.Prenom.IndexOf(motcle) != -1)
                    {
                        Resultat.Add(projet);
                    }
                }
                foreach (Livrable livrable in projet.LivrablesAttendus)
                {
                    if (livrable.NomLivrable.IndexOf(motcle) != -1)
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

            Console.WriteLine("\n\n----- Type de projet-----");
            Console.WriteLine("Voici la liste des types de projets possible : ");
            int j = 1;
            foreach (string t in ListeTypeProjet)
            {
                Console.WriteLine("{0} - {1}", j, t.ToString());
                j++;
            }
            Console.WriteLine("{0} - Ajouter un nouveau type de projet", j);

            Console.WriteLine("\nChoisissez votre type de projet : ");
            int choixTypeProjet = Convert.ToInt32(Console.ReadLine());

            while (choixTypeProjet < 0 || choixTypeProjet > j)
            {
                Console.WriteLine("Je n'ai pas compris votre choix");
                Console.WriteLine("Choisissez votre type de projet : ");
                choixTypeProjet = Convert.ToInt32(Console.ReadLine());
            }


            string nvType;
            if (choixTypeProjet == j)
            {
                Console.Write("\nType de projet : ");
                nvType = Console.ReadLine();
                while (nvType == "")
                {
                    Console.WriteLine("Vous devez remplir le champ");
                    Console.Write("Type de projet : ");
                    nvType = Console.ReadLine();
                }
                ListeTypeProjet.Add(nvType);
            }
            else
            {
                nvType = ListeTypeProjet[choixTypeProjet - 1];
            }
            NouveauProjet(nvType);            
        }


        public void NouveauProjet(string nvType)
        {
            //THEME
            Console.Write("Thème : ");
            string nvTheme = Console.ReadLine();
            while (nvTheme == "")
            {
                Console.WriteLine("Vous devez remplir le champ");
                Console.Write("Thème : ");
                nvTheme = Console.ReadLine();
            }

            //SUJET LIBRE
            //On impose le sujet libre ou non selon le type de projet
            //Si la personne crée un nouveau projet, elle choisit si le sujet est libre ou non
            bool nvSujetLibre;
            if (nvType == "Projet transpromo" || nvType == "PFE")
            {
                Console.WriteLine("Sujet libre : Oui");
                nvSujetLibre = true;
            }
            else if (nvType =="Projet transdisciplinaire" || nvType == "RAO" || nvType == "Projet web")
            {
                Console.WriteLine("Sujet libre : Non");
                nvSujetLibre = false;
            }
            else
            {
                Console.Write("Sujet libre (O/N) : ");
                string rep = Console.ReadLine();
                while (rep != "O" && rep != "N")
                {
                    Console.WriteLine("Je n'ai pas compris votre réponse.");
                    Console.Write("Sujet libre (O/N) : ");
                    rep = Console.ReadLine();
                }
                if (rep == "O")
                    nvSujetLibre = true;
                else
                    nvSujetLibre = false;
            }

            //DATES
            //DateTime.Parse permet de vérifier si le string entré par l'utilisateur est bien une date. Si ça l'est, il le met dans dateDebutValide
            Console.Write("Date de début (jj/mm/aaaa) : ");
            DateTime dateDebutValide;
            bool resultDateDebut = DateTime.TryParse(Console.ReadLine(), out dateDebutValide);
            while (resultDateDebut == false)
            {
                Console.WriteLine("Le format est incorrect. Veuillez réessayer");
                Console.Write("Date de début (jj/mm/aaaa) : ");
                resultDateDebut = DateTime.TryParse(Console.ReadLine(), out dateDebutValide);
            }

            Console.Write("Date de fin (jj/mm/aaaa) : ");
            DateTime dateFinValide;
            bool resultDateFin = DateTime.TryParse(Console.ReadLine(), out dateFinValide);
            //On vérifie que l'utilisateur a bien rentré une date ET que la date est bien ultérieure à la date de début
            while (resultDateFin == false || DateTime.Compare(dateDebutValide, dateFinValide) > 0)
            {
                if (resultDateFin == false)
                    Console.WriteLine("Le format est incorrect. Veuillez réessayer");
                else
                    Console.WriteLine("La date de fin est inférieur à la date de début. Veuillez réessayer");

                Console.Write("Date de fin (jj/mm/aaaa) : ");
                resultDateFin = DateTime.TryParse(Console.ReadLine(), out dateFinValide);
            }

            //INETREVENANTS ET ROLES
            List<Intervenant> nvListeIntervenant = ChoixIntervenant(nvType);
            List<Role> nvListeRole = ChoixRole(nvListeIntervenant);

            //MATIERES ET LIVRABLES
            //On impose les matières pour rao et projet web.
            //On demande à l'utilisateur de choisir ses matières pour les autres projets
            List<Matiere> nvListeMatiere;
            List<Livrable> nvListeLivrable;
            if (nvType != "RAO" && nvType != "Projet web")
            {
                nvListeMatiere = ChoixMatiere();
                nvListeLivrable = ChoixLivrable();
            }
            else
            {
                if (nvType == "RAO")
                {
                    nvListeMatiere = new List<Matiere> { ListeMatieres[2] }; //correspond à GESP
                    nvListeLivrable = new List<Livrable> { ListeLivrables[2], ListeLivrables[4] }; //correspond à GESP
                    Console.Write("Matières : {0}", nvListeMatiere[0]);
                    Console.Write("Livrables : {0}, {1}", nvListeLivrable[0], nvListeLivrable[1]);

                }
                else
                {
                    nvListeMatiere = new List<Matiere> { ListeMatieres[6], ListeMatieres[2] }; //correspond à info et GESP
                    nvListeLivrable = new List<Livrable> { ListeLivrables[0], ListeLivrables[2] }; //correspond à GESP
                    Console.WriteLine("Matières : {0}", nvListeMatiere[0], nvListeMatiere[1]);
                    Console.WriteLine("Livrables : {0}, {1}", nvListeLivrable[0], nvListeLivrable[1]);
                }

            }

            //CREATION PROJET
            Projet nouveauProjet = new Projet(nvType, nvTheme, nvSujetLibre, dateDebutValide, dateFinValide, nvListeIntervenant, nvListeRole, nvListeMatiere, nvListeLivrable);
            ListeProjets.Add(nouveauProjet);
            Console.WriteLine("\nLe projet a bien été ajouté !");
        }




        public List<Intervenant> ChoixIntervenant(string typeProjet)
        {
            //Création de la liste d'intervenants associée au projet
            //On affiche la liste des intervenants existants
            //L'utilisateur selectione les intervenants qu'il souhaite, quand il a fini il tape 0 pour sortir de la boucle
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
                
                //On vérifie que le numéro demandé existe bien
                while (choixIntervenant < 0 || choixIntervenant > j - 1)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("Ajouter un intervenant (entrez 0 pour finir) : ");
                    choixIntervenant = Convert.ToInt32(Console.ReadLine());
                }

                //On vérifie que l'intervenant n'a pas déjà été ajouté
                if (choixIntervenant != 0)
                {
                    bool existeDeja = false;
                    foreach (Intervenant i in nvListeIntervenant)
                    {
                        if (ListeIntervenants[choixIntervenant - 1] == i)
                            existeDeja = true;
                    }

                    if (existeDeja == true)
                        Console.WriteLine("L'intervenant a déjà été ajouté");
                    else
                    {
                        nvListeIntervenant.Add(ListeIntervenants[choixIntervenant - 1]);
                        Console.WriteLine("\tL'intervenant a bien été ajouté");
                    }
                }
            }
            return nvListeIntervenant;
        }


        public List<Role> ChoixRole(List<Intervenant> listeIntervenant)
        {
            //Création de la liste d'intervenants associée au projet
            //On affiche la liste des intervenants existants
            //L'utilisateur slectione les intervenants qu'il souhaite, quand il a fini il tape 0 pour sortir de la boucle
            Console.WriteLine("\n\n----- Rôles des Intervenants -----");
            List<Role> nvListeRole = new List<Role> { };
            Console.WriteLine("Voici la liste des rôles possibles : ");
            int j = 1;
            foreach (Role i in ListeRoles)
            {
                Console.WriteLine("{0} - {1}", j, i.ToString());
                j++;
            }
            Console.WriteLine("{0} - Ajouter un nouveau rôle", j);

            Console.Write("Attribuez un rôle pour chaque intervenant : \n");


            int choixRole = 1;

            foreach(Intervenant interv in listeIntervenant)
            {
                Console.Write("{0} {1} a pour rôle : ", interv.Prenom, interv.Nom);
                choixRole = Convert.ToInt32(Console.ReadLine());

                //On vérifie que le numéro demandé existe bien

                while (choixRole <= 0 || choixRole > j)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("{0} {1} a pour rôle : ", interv.Prenom, interv.Nom);
                    choixRole = Convert.ToInt32(Console.ReadLine());
                }
                if (choixRole == j)
                {
                    Console.Write("Nom du nouveau rôle : ");
                    Role nouveauRole = new Role(Console.ReadLine());
                    ListeRoles.Add(nouveauRole);
                    nvListeRole.Add(ListeRoles[choixRole - 1]);
                    Console.WriteLine("\t{0} {1} est bien {2} ", interv.Prenom, interv.Nom, nouveauRole.NomRole);
                }
                else if (choixRole != 0)
                {
                    nvListeRole.Add(ListeRoles[choixRole - 1]);
                    Console.WriteLine("\t{0} {1} est bien {2} ", interv.Prenom, interv.Nom, ListeRoles[choixRole - 1].NomRole);
                }
            }
            return nvListeRole;
        }


        public List<Matiere> ChoixMatiere()
        {
            Console.WriteLine("\n\n----- Matières concernées-----");
            List<Matiere> nvListeMatieres = new List<Matiere> { };
            Console.WriteLine("Voici la liste des matières possible : ");
            int j = 1;
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

                //On vérifie que le numéro demandé existe bien
                while (choixMatiere < 0 || choixMatiere > j - 1)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("Ajouter une matière (entrez 0 pour finir) : ");
                    choixMatiere = Convert.ToInt32(Console.ReadLine());
                }

                if (choixMatiere != 0)
                {
                    bool existeDeja = false;
                    foreach (Matiere m in nvListeMatieres)
                    {
                        if (ListeMatieres[choixMatiere - 1] == m)
                            existeDeja = true;
                    }

                    if (existeDeja == true)
                        Console.WriteLine("La matière a déjà été ajouté");
                    else
                    {
                        nvListeMatieres.Add(ListeMatieres[choixMatiere - 1]);
                        Console.WriteLine("\tLa matière a bien été ajoutée");
                    }
                }

            }
            return nvListeMatieres;
        }


        public List<Livrable> ChoixLivrable()
        {
            //Exactement la même chose pour les livrables
            Console.WriteLine("\n\n----- Livrables-----");
            List<Livrable> nvListeLivrable = new List<Livrable> { };
            Console.WriteLine("Voici la liste des livrables possible : ");
            int j = 1;
            foreach (Livrable l in ListeLivrables)
            {
                Console.WriteLine("{0} - {1}", j, l.ToString());
                j++;
            }
            Console.WriteLine("{0} - Ajouter un nouveau livrable", j);

            int choixLivrable = 1;
            while (choixLivrable != 0)
            {
                Console.Write("Ajouter un livrable (entrez 0 pour finir) : ");
                choixLivrable = Convert.ToInt32(Console.ReadLine());

                //On vérifie que le numéro demandé existe bien
                //Cette fois c'est >j car il y a une option en plus(ajouter un livrable)
                while (choixLivrable < 0 || choixLivrable > j)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("Ajouter un livrable (entrez 0 pour finir) : ");
                    choixLivrable = Convert.ToInt32(Console.ReadLine());
                }
                if (choixLivrable != 0)
                {
                    //Si l'utilisateur veut ajouter un nouveau livrable
                    if (choixLivrable == j)
                    {
                        Console.Write("Nom du nouveau livrable : ");
                        Livrable nouveauLivrable = new Livrable(Console.ReadLine());
                        ListeLivrables.Add(nouveauLivrable);
                        nvListeLivrable.Add(ListeLivrables[choixLivrable - 1]);
                        Console.WriteLine("\tLe livrable a bien été ajouté");
                    }
                    else
                    {
                        //Sinon on vérifie que le livrable n'a pas déjà été ajouté
                        bool existeDeja = false;
                        foreach (Livrable l in nvListeLivrable)
                        {
                            if (ListeLivrables[choixLivrable - 1] == l)
                                existeDeja = true;
                        }
                        if (existeDeja == true)
                            Console.WriteLine("Le livrable a déjà été ajouté");
                        else if (choixLivrable != j)
                        {
                            nvListeLivrable.Add(ListeLivrables[choixLivrable - 1]);
                            Console.WriteLine("\tLe livrable a bien été ajouté");
                        }
                    }
                    
                    
                }
                
            }

            return nvListeLivrable;
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
            while (choixSupression < 0 || choixSupression > j-1)
            {
                Console.Write("Je n'ai pas compris votre choix");
                Console.Write("\nLequel voulez-vous supprimer ?  ");
                choixSupression = Convert.ToInt32(Console.ReadLine());
            }
            ListeProjets.RemoveAt(choixSupression - 1);

            Console.WriteLine("Le projet a bien été supprimé !");

        }


        public void Sauvegarder(string fichierXML)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Catalogue));
            TextWriter writer = new StreamWriter(fichierXML);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public void ReinitialiserCatalogue()
        {
            //On supprime le catalogue utilisateur
            //File.Delete("Users\\juliettesquirol\\Documents\\GitHub\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml");
            File.Delete("C:\\Users\\leagr\\Source\\Repos\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml");

            //On copie le catalogue de base, et on on le colle en créant un nouveau catalogue utilisateur
            //string sourceFile = "Users\\juliettesquirol\\Documents\\GitHub\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\catalogueBase.xml";
            string sourceFile = "C:\\Users\\leagr\\Source\\Repos\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\catalogueBase.xml";
            
            //string destinationFile = "Users\\juliettesquirol\\Documents\\GitHub\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml";
            string destinationFile = "C:\\Users\\leagr\\Source\\Repos\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml";
            File.Copy(sourceFile, destinationFile);

        }

    }
}