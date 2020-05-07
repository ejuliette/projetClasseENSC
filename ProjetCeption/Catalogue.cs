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
        public List<String> ListeTypeProjet { get; set; }
        public List<Eleve> ListeEleves { get; set; }
        public List<Enseignant> ListeEnseignants { get; set; }
        public List<Externe> ListeExternes { get; set; }
        public List<Intervenant> ListeIntervenants { get; set; }
        public List<Role> ListeRoles { get; set; }
        public List<Matiere> ListeMatieres { get; set; }
        public List<Livrable> ListeLivrables { get; set; }
        public List<Projet> ListeProjets { get; set; }

        public Catalogue()
        {
        }

        public List<Projet> RechercherParIntervenant()
        {
            //On demande le nom et prénom à rechercher
            Console.Write("\nNom : ");
            string nom = Console.ReadLine();
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();

            //On effectue la recherche en stockant les projets dans la liste Resultat
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                foreach (Intervenant intervenant in projet.IntervenantsConcernes)
                {
                    if (intervenant.Nom == nom && intervenant.Prenom == prenom)
                    {
                        Resultat.Add(projet);
                    }
                }
            }

            //On affiche le résultat
            if (Resultat.Count != 0)
            {
                foreach (Projet projet in Resultat)
                    Console.WriteLine(projet.ToString());
            }
            else
                Console.WriteLine("Aucun résultat ne correspond à votre recherche");
            return Resultat;
        }


        public List<Projet> RechercherParAnnee()
        {
            //On demande l'année à rechercher
            Console.Write("\nAnnée : ");
            int annee = Convert.ToInt32(Console.ReadLine());

            //On effectue la recherche en stockant les projets dans la liste Resultat
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                if (projet.DateDebut.Year == annee || projet.DateFin.Year == annee)
                    Resultat.Add(projet);
            }

            //On affiche le résultat
            if (Resultat.Count != 0)
            {
                foreach (Projet projet in Resultat)
                    Console.WriteLine(projet.ToString());
            }
            else
                Console.WriteLine("Aucun résultat ne correspond à votre recherche");
            return Resultat;
        }

        public List<Projet> RechercherParMatiere()
        {
            //On présente la liste des matières possible
            Console.WriteLine("Voici la liste des matières possible : ");
            int j = 1;
            foreach (Matiere m in ListeMatieres)
            {
                Console.WriteLine("{0} - {1}", j, m.ToString());
                j++;
            }
            //L'utilisateur en choisi une parmi celles proposées (on vérifie qu'il a entré un numéro valide)
            Console.Write("Rechercher la matière : ");
            int numMatiere = Convert.ToInt32(Console.ReadLine());
            while (numMatiere < 1 || numMatiere > j - 1)
            {
                Console.WriteLine("Je n'ai pas compris votre choix");
                Console.Write("Rechercher la matière : ");
                numMatiere = Convert.ToInt32(Console.ReadLine());
            }
            //On attribue le numéro entré par l'utilisateur à une matière
            Matiere matiereRecherchee = ListeMatieres[numMatiere - 1];

            //On effectue la recherche en stockant les projets dans la liste Resultat
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                foreach (Matiere matiere in projet.MatieresConcernees)
                {
                    if (matiere.NomMatiere == matiereRecherchee.NomMatiere)
                    {
                        Resultat.Add(projet);
                    }
                }
            }

            //On affiche le résultat
            if (Resultat.Count != 0)
            {
                foreach (Projet projet in Resultat)
                    Console.WriteLine(projet.ToString());
            }
            else
                Console.WriteLine("Aucun résultat ne correspond à votre recherche");

            return Resultat;
        }

        public List<Projet> RechercherParMotCle()
        {
            Console.Write("\nMot clé : ");
            string motcle = Console.ReadLine();

            //Ne fais pas la recherche sur les années
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                if (projet.TypeProjet.IndexOf(motcle) != -1)
                    Resultat.Add(projet);

                if (projet.Theme.IndexOf(motcle) != -1)
                    Resultat.Add(projet);

                foreach (Matiere matiere in projet.MatieresConcernees)
                {
                    if (matiere.NomMatiere.IndexOf(motcle) != -1 || matiere.CodeSyllabus.IndexOf(motcle) !=-1)
                        Resultat.Add(projet);
                }
                foreach (Intervenant intervenant in projet.IntervenantsConcernes)
                {
                    if (intervenant.Nom.IndexOf(motcle) != -1 || intervenant.Prenom.IndexOf(motcle) != -1)
                        Resultat.Add(projet);
                }
                foreach (Livrable livrable in projet.LivrablesAttendus)
                {
                    if (livrable.NomLivrable.IndexOf(motcle) != -1)
                        Resultat.Add(projet);
                }
            }

            if (Resultat.Count != 0)
            {
                foreach (Projet projet in Resultat)
                    Console.WriteLine(projet.ToString());
            }
            else
                Console.WriteLine("Aucun résultat ne correspond à votre recherche");

            return Resultat;
        }



        
        public void AjouterProjet()
        {
            Console.WriteLine("\nVeuillez rentrer toutes les informations nécessaires :");

            //TYPE DE PROJET
            Console.WriteLine("\n\n----- Type de projet-----");
            string typeProjet = choixTypeProjet();

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
            if (typeProjet == "Projet transpromo" || typeProjet == "PFE")
            {
                Console.WriteLine("Pour ce type de projet, le sujet est libre.");
                nvSujetLibre = true;
            }
            else if (typeProjet == "Projet transdisciplinaire" || typeProjet == "RAO" || typeProjet == "Projet web")
            {
                Console.WriteLine("Pour ce type de projet, le sujet est imposé.");
                nvSujetLibre = false;
            }
            else
            {
                nvSujetLibre = choixSujetLibre();
            }

            //DATES
            DateTime dateDebut = choixDates()[0];
            DateTime dateFin = choixDates()[1];


            //INETERVENANTS ET ROLES
            List<Intervenant> nvListeIntervenant = ChoixIntervenant();
            List<Role> nvListeRole = ChoixRole(nvListeIntervenant);


            //MATIERES ET LIVRABLES
            //On impose les matières pour rao et projet web.
            //On demande à l'utilisateur de choisir ses matières pour les autres projets
            List<Matiere> nvListeMatiere;
            List<Livrable> nvListeLivrable;
            if (typeProjet != "RAO" && typeProjet != "Projet web")
            {
                nvListeMatiere = ChoixMatiere();
                nvListeLivrable = ChoixLivrable();
            }
            else
            {
                if (typeProjet == "RAO")
                {
                    nvListeMatiere = new List<Matiere> { ListeMatieres[2] }; //correspond à GESP
                    nvListeLivrable = new List<Livrable> { ListeLivrables[2], ListeLivrables[4] }; //correspond à 
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
            Projet nouveauProjet = new Projet(typeProjet, nvTheme, nvSujetLibre, dateDebut, dateFin, nvListeIntervenant, nvListeRole, nvListeMatiere, nvListeLivrable);
            ListeProjets.Add(nouveauProjet);
            Console.WriteLine("\nLe projet a bien été ajouté !");

        }


        public string choixTypeProjet()
        {
            Console.WriteLine("Voici la liste des types de projets possible : ");
            int j = 1;
            foreach (string t in ListeTypeProjet)
            {
                Console.WriteLine("{0} - {1}", j, t.ToString());
                j++;
            }
            Console.WriteLine("{0} - Ajouter un nouveau type de projet", j);

            Console.Write("\nChoisissez votre type de projet : ");
            int choixTypeProjet = Convert.ToInt32(Console.ReadLine());

            while (choixTypeProjet < 0 || choixTypeProjet > j)
            {
                Console.WriteLine("Je n'ai pas compris votre choix");
                Console.Write("Choisissez votre type de projet : ");
                choixTypeProjet = Convert.ToInt32(Console.ReadLine());
            }

            string typeProjet;
            if (choixTypeProjet == j)
            {
                Console.Write("\nType de projet : ");
                typeProjet = Console.ReadLine();
                while (typeProjet == "")
                {
                    Console.WriteLine("Vous devez remplir le champ");
                    Console.Write("Type de projet : ");
                    typeProjet = Console.ReadLine();
                }
                ListeTypeProjet.Add(typeProjet);
            }
            else
            {
                typeProjet = ListeTypeProjet[choixTypeProjet - 1];
            }
            return typeProjet;
        }

        public bool choixSujetLibre()
        {
            bool sujetLibre;
            Console.Write("Sujet libre (O/N) : ");
            string rep = Console.ReadLine();
            while (rep != "O" && rep != "N")
            {
                Console.WriteLine("Je n'ai pas compris votre réponse.");
                Console.Write("Sujet libre (O/N) : ");
                rep = Console.ReadLine();
            }
            if (rep == "O")
                sujetLibre = true;
            else
                sujetLibre = false;

            return sujetLibre;
        }


        public DateTime[] choixDates()
        {
            DateTime[] tabDates = new DateTime[2];
            Console.Write("Date de début (jj/mm/aaaa) : ");
            DateTime dateDebutValide;
            bool resultDateDebut = DateTime.TryParse(Console.ReadLine(), out dateDebutValide);
            while (resultDateDebut == false)
            {
                Console.WriteLine("Le format est incorrect. Veuillez réessayer");
                Console.Write("Date de début (jj/mm/aaaa) : ");
                resultDateDebut = DateTime.TryParse(Console.ReadLine(), out dateDebutValide);
            }
            tabDates[0] = dateDebutValide;

            Console.Write("Date de fin (jj/mm/aaaa) : ");
            DateTime dateFinValide;
            bool resultDateFin = DateTime.TryParse(Console.ReadLine(), out dateFinValide);
            //On vérifie que l'utilisateur a bien rentré une date ET que la date est bien ultérieure à la date de début
            while (resultDateFin == false || DateTime.Compare(dateDebutValide, dateFinValide) > 0)
            {
                if (resultDateFin == false)
                    Console.WriteLine("Le format est incorrect. Veuillez réessayer");
                else
                    Console.WriteLine("La date de fin est antérieure à la date de début. Veuillez réessayer");

                Console.Write("Date de fin (jj/mm/aaaa) : ");
                resultDateFin = DateTime.TryParse(Console.ReadLine(), out dateFinValide);
            }
            tabDates[1] = dateFinValide;

            return tabDates;
        }


        public List<Intervenant> ChoixIntervenant()
        {
            //Création de la liste d'intervenants associée au projet
            //On affiche la liste des intervenants existants
            //L'utilisateur selectione les intervenants qu'il souhaite, quand il a fini il tape 0 pour sortir de la boucle
            Console.WriteLine("\n\n----- Intervenants-----");
            List<Intervenant> nvListeIntervenant = new List<Intervenant> { };

            int choix = 0;


            while (choix != 4)
            {
                Console.WriteLine("\n1 - Rechercher un intervenant dans le catalogue");
                Console.WriteLine("2 - Voir la liste des intervenants existants");
                Console.WriteLine("3 - Ajouter un nouvel intervenant");
                Console.WriteLine("4 - J'ai fini d'ajouter des intervenants pour ce projet");

                Console.Write("Votre choix : ");
                choix = Convert.ToInt32(Console.ReadLine());

                if (choix == 1)
                {
                    RechercherIntervenant(nvListeIntervenant);
                }

                if (choix == 2)
                {
                    AfficherIntervenants(nvListeIntervenant);
                }

                if (choix == 3)
                {
                    AjouterIntervenant(nvListeIntervenant);
                }
            }

            return nvListeIntervenant;
        }



        public void AjouterIntervenant(List<Intervenant> nvListeIntervenant)
        {
            Console.WriteLine("Ajout d'un nouvel intervenant");

            Console.Write("Nom : ");
            string Nom = Console.ReadLine();
            Console.Write("Prénom : ");
            string Prenom = Console.ReadLine();

            bool existeDeja = false;
            foreach (Intervenant i in ListeIntervenants)
            {
                if (Nom == i.Nom && Prenom == i.Prenom)
                {
                    existeDeja = true;
                    break;
                }
            }
            if (existeDeja == true)
                Console.WriteLine("Cet intervenant existe déjà !");
            else
            {
                Console.WriteLine("L'intervenant est un : \n 1 - Elève de l'ENSC \n 2 - Enseignant de l'ENSC \n 3 - Extérieur à l'ENSC");
                int TypeIntervenant = Convert.ToInt32(Console.ReadLine());
                if (TypeIntervenant == 1)
                {
                    Console.WriteLine("Promotion de l'élève (4 chiffres) : ");
                    int Promo = Convert.ToInt32(Console.ReadLine());
                    Eleve nouvelIntervenant = new Eleve(Nom, Prenom, Promo);
                    ListeIntervenants.Add(nouvelIntervenant);
                    nvListeIntervenant.Add(ListeIntervenants[ListeIntervenants.IndexOf(nouvelIntervenant)]);
                    Console.WriteLine("\tL'intervenant a bien été ajouté au projet !");
                }
                if (TypeIntervenant == 2)
                {

                    List<Matiere> listmat = new List<Matiere> { };
                    Console.WriteLine("Voici la liste des matières possibles : ");
                    int k = 1;
                    foreach (Matiere m in ListeMatieres)
                    {
                        Console.WriteLine("{0} - {1}", k, m.ToString());
                        k++;
                    }
                    int choixMatiere = 1;

                    while (choixMatiere != 0)
                    {
                        Console.Write("Sélectionnez les matières de cet enseignant (entrez 0 pour finir) : ");
                        choixMatiere = Convert.ToInt32(Console.ReadLine());

                        //On vérifie que le numéro demandé existe bien
                        while (choixMatiere < 0 || choixMatiere > k - 1)
                        {
                            Console.WriteLine("Je n'ai pas compris votre choix");
                            Console.Write("Ajouter une matière (entrez 0 pour finir) : ");
                            choixMatiere = Convert.ToInt32(Console.ReadLine());
                        }

                        if (choixMatiere != 0)
                        {
                            bool matExisteDeja = false;
                            foreach (Matiere m in listmat)
                            {
                                if (ListeMatieres[choixMatiere - 1] == m)
                                    existeDeja = true;
                            }

                            if (matExisteDeja == true)
                                Console.WriteLine("La matière a déjà été ajouté");
                            else
                            {
                                listmat.Add(ListeMatieres[choixMatiere - 1]);
                                Console.WriteLine("\tLa matière a bien été ajoutée");
                            }
                        }
                    }
                    Enseignant nouvelIntervenant = new Enseignant(Nom, Prenom, listmat);
                    ListeIntervenants.Add(nouvelIntervenant);
                    nvListeIntervenant.Add(ListeIntervenants[ListeIntervenants.IndexOf(nouvelIntervenant)]);
                    Console.WriteLine("\tL'intervenant a bien été ajouté au projet !");

                }
                if (TypeIntervenant == 3)
                {
                    Console.WriteLine("Métier : ");
                    string Metier = Console.ReadLine();
                    Externe nouvelIntervenant = new Externe(Nom, Prenom, Metier);
                    ListeIntervenants.Add(nouvelIntervenant);
                    nvListeIntervenant.Add(ListeIntervenants[ListeIntervenants.IndexOf(nouvelIntervenant)]);
                    Console.WriteLine("\tL'intervenant a bien été ajouté au projet !");
                }
            }
        }


        public void RechercherIntervenant(List<Intervenant> nvListeIntervenant)
        {
            Console.WriteLine("Rechercher d'intervenants");
            Console.Write("Nom : ");
            string NomRecherche = Console.ReadLine();
            Console.Write("Prénom : ");
            string PrenomRecherche = Console.ReadLine();


            List<Intervenant> Resultat = new List<Intervenant>();
            foreach (Intervenant intervenant in ListeIntervenants)
            {
                if (intervenant.Nom.IndexOf(NomRecherche) != -1 || intervenant.Prenom.IndexOf(PrenomRecherche) != -1)
                    Resultat.Add(intervenant);
            }

            int j = 0;
            foreach (Intervenant i in Resultat)
            {
                j++;
                Console.WriteLine("{0} - {1}", j, i.ToString());
            }
            if (j == 0)
                Console.WriteLine("Aucun résultat pour cette recherche.");
            else
            {
                Console.Write("Ajouter l'intervenant (entrez 0 pour annuler) : ");
                int choixIntervenant = Convert.ToInt32(Console.ReadLine());

                if (choixIntervenant != 0)
                {
                    //On vérifie que le numéro demandé existe bien
                    while (choixIntervenant < 0 || choixIntervenant > j)
                    {
                        Console.WriteLine("Je n'ai pas compris votre choix");
                        Console.Write("\nAjouter l'intervenant (entrez 0 pour annuler) : ");
                        choixIntervenant = Convert.ToInt32(Console.ReadLine());
                    }

                    bool existeDeja = false;
                    foreach (Intervenant i in nvListeIntervenant)
                    {
                        if (Resultat[choixIntervenant - 1] == i)
                            existeDeja = true;
                    }

                    if (existeDeja == true)
                        Console.WriteLine("L'intervenant a déjà été ajouté");
                    else
                    {
                        nvListeIntervenant.Add(Resultat[choixIntervenant - 1]);
                        Console.WriteLine("\tL'intervenant a bien été ajouté au projet !");
                    }
                }
            }
        }

        public void AfficherIntervenants(List<Intervenant> nvListeIntervenant)
        {
            Console.WriteLine("Voici la liste d'intervenants du catalogue : ");

            int j = 0;
            foreach(Intervenant i in ListeIntervenants)
            {
                j++;
                Console.WriteLine("{0} - {1}", j, i.ToString());
            }

            Console.Write("\nVous pouvez ajouter plusieurs intervenants. Tapez 0 pour finir");
            int choixIntervenant = 1;
            while (choixIntervenant != 0)
            {
                Console.Write("\nAjouter un intervenant : ");
                choixIntervenant = Convert.ToInt32(Console.ReadLine());

                //On vérifie que le numéro demandé existe bien
                while (choixIntervenant < 0 || choixIntervenant > j)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("Ajouter un intervenant : ");
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
                        Console.WriteLine("\tL'intervenant a bien été ajouté !");
                    }
                }
            }
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

            foreach (Intervenant interv in listeIntervenant)
            {
                Console.Write("{0} {1} a pour rôle : ", interv.Prenom, interv.Nom);
                int choixRole = Convert.ToInt32(Console.ReadLine());
                
                //On vérifie que le numéro demandé existe bien
                while (choixRole <= 0 || choixRole > j)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("{0} {1} a pour rôle : ", interv.Prenom, interv.Nom);
                    choixRole = Convert.ToInt32(Console.ReadLine());
                }
                //Possibilité de créer un nouveau rôle
                if (choixRole == j)
                {
                    Console.Write("Nom du nouveau rôle : ");
                    Role nouveauRole = new Role(Console.ReadLine());
                    ListeRoles.Add(nouveauRole);
                }
                nvListeRole.Add(ListeRoles[choixRole - 1]);
                Console.WriteLine("\t{0} {1} est bien {2} ", interv.Prenom, interv.Nom, ListeRoles[choixRole - 1].NomRole);
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


        public void ModifierProjet()
        {
            Console.WriteLine("Voici la liste des projets : ");
            int j = 1;
            foreach (Projet projet in ListeProjets)
            {
                Console.WriteLine("{0} - {1}\n", j, projet.ToString());
                j++;
            }
            Console.Write("\nLequel voulez-vous modifier ?  ");
            int choixProjet = Convert.ToInt32(Console.ReadLine());
            while (choixProjet < 0 || choixProjet > j - 1)
            {
                Console.Write("Je n'ai pas compris votre choix");
                Console.Write("\nLequel voulez-vous modifier ?  ");
                choixProjet = Convert.ToInt32(Console.ReadLine());
            }
            List<string> listeElements = new List<string> { "type de projet", "thème", "sujet libre", "dates", "intervenants et rôles", "matieres", "livrables"};
            int choixModif = 1;
            while(choixModif !=0)
            {
                foreach (string e in listeElements)
                {
                    Console.WriteLine("{0} - {1}", listeElements.IndexOf(e) + 1, e.ToString());
                }
                Console.Write("\nQue voulez-vous modifier (tapez 0 pour finir) ?  ");
                choixModif = Convert.ToInt32(Console.ReadLine());
                while (choixModif < 0 || choixModif > listeElements.Count)
                {
                    Console.Write("Je n'ai pas compris votre choix");
                    Console.Write("\nLequel voulez-vous modifier ?  ");
                    choixModif = Convert.ToInt32(Console.ReadLine());
                }


                if (choixModif == 1)
                {
                    ListeProjets[choixProjet - 1].TypeProjet = choixTypeProjet();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                if (choixModif == 2)
                {
                    Console.Write("Thème : ");
                    string theme = Console.ReadLine();
                    while (theme == "")
                    {
                        Console.WriteLine("Vous devez remplir le champ");
                        Console.Write("Thème : ");
                        theme = Console.ReadLine();
                    }
                    ListeProjets[choixProjet - 1].Theme = theme;
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                if (choixModif == 3)
                {
                    ListeProjets[choixProjet - 1].SujetLibre = choixSujetLibre();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                if (choixModif == 4)
                {
                    DateTime[] tabDates = choixDates();
                    ListeProjets[choixProjet - 1].DateDebut = tabDates[0];
                    ListeProjets[choixProjet - 1].DateFin = tabDates[1];
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                if (choixModif == 5)
                {
                    List<Intervenant> listeInterv = ChoixIntervenant();
                    ListeProjets[choixProjet - 1].IntervenantsConcernes = listeInterv;
                    ListeProjets[choixProjet - 1].IntervenantsRoles = ChoixRole(listeInterv);
                    ListeProjets[choixProjet - 1].NbIntervenants = listeInterv.Count;
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                if (choixModif == 6)
                {
                    ListeProjets[choixProjet - 1].MatieresConcernees = ChoixMatiere();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                if (choixModif == 7)
                {
                    ListeProjets[choixProjet - 1].LivrablesAttendus = ChoixLivrable();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
            }
            

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