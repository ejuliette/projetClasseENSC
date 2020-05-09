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
                        Resultat.Add(projet);
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
            //On présente la liste des matières possibles
            Console.WriteLine("Voici la liste des matières possibles : ");
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
            //La fonction B.IndexOf(A) renvoie la position de la chaîne de caractères A
            //au sein de la chaîne de caractères B et -1 si A n'apparaît pas dans B
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

            //Choix du Type de Projet
            Console.WriteLine("\n\n----- Type de projet-----");
            string typeProjet = choixTypeProjet();

            //Choix du thème 
            Console.Write("Thème : ");
            string nvTheme = Console.ReadLine();
            while (nvTheme == "")
            {
                Console.WriteLine("Vous devez remplir le champ");
                Console.Write("Thème : ");
                nvTheme = Console.ReadLine();
            }

            //Choix du sujet
            bool nvSujetLibre;
            //On impose le sujet libre ou non selon certains types de projet
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
            else //Si la personne crée un nouveau projet, elle choisit si le sujet est libre ou non
            {
                nvSujetLibre = choixSujetLibre();
            }

            //Choix des dates
            DateTime[] tabDates = choixDates();
            DateTime dateDebut = tabDates[0];
            DateTime dateFin = tabDates[1];


            //Choix des intervenants et de leur rôle
            List<Intervenant> nvListeIntervenant = ChoixIntervenant();
            List<Role> nvListeRole = ChoixRole(nvListeIntervenant);


            //Choix des matières et livrables
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
                    nvListeLivrable = new List<Livrable> { ListeLivrables[2], ListeLivrables[4] }; //correspond à Rapport et Prestation orale
                    Console.WriteLine("Matières : {0}", nvListeMatiere[0]);
                    Console.WriteLine("Livrables : {0}, {1}", nvListeLivrable[0], nvListeLivrable[1]);

                }
                else
                {
                    nvListeMatiere = new List<Matiere> { ListeMatieres[6], ListeMatieres[2] }; //correspond à Communication Web et GESP
                    nvListeLivrable = new List<Livrable> { ListeLivrables[0], ListeLivrables[2] }; //correspond à Site web et Rapport
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
            //Affichage des choix possibles (sélection parmi les types de projet 
            //existants ou ajout d'un nouveau type de projet)
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

            //Vérification que le choix entré par l'utilisateur est correct
            while (choixTypeProjet < 0 || choixTypeProjet > j)
            {
                Console.WriteLine("Je n'ai pas compris votre choix");
                Console.Write("Choisissez votre type de projet : ");
                choixTypeProjet = Convert.ToInt32(Console.ReadLine());
            }

            //Création d'un nouveau type de projet 
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
                //Insertion du nouveau type de projet à la liste
                ListeTypeProjet.Add(typeProjet);
            }
            else
            {
                //Sélection d'un type de projet existant
                typeProjet = ListeTypeProjet[choixTypeProjet - 1];
            }
            //Renvoie le type de projet choisi
            return typeProjet;
        }

        public bool choixSujetLibre()
        {
            bool sujetLibre;
            //Choix du sujet libre (O = Oui) ou imposé (N = Non)
            Console.Write("Sujet libre (O/N) : ");
            string rep = Console.ReadLine();

            //Vérification de la rpéonse entrée par l'utilisateur
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

            //Renvoie le booléen indiquant si le sujet est libre ou non
            return sujetLibre;
        }


        public DateTime[] choixDates()
        {
            //Initialisation du tableau contenant les dates de début et de fin de projet
            DateTime[] tabDates = new DateTime[2];
            Console.Write("Date de début (jj/mm/aaaa) : ");
            //Vérification si l'entrée de l'utilisateur correspond au format souahité
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
            //Renvoie le tableau contenant les dates de début et de fin du projet
            return tabDates;
        }


        public List<Intervenant> ChoixIntervenant()
        {
            //Création de la liste d'intervenants associée au projet : nvListeIntervenant
            //On affiche la liste des intervenants existants : ListeIntervenants
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
            //L'utilisateur entre le nom et le prénom de l'intervenant qu'il souhaite créer
            Console.Write("Nom : ");
            string Nom = Console.ReadLine();
            Console.Write("Prénom : ");
            string Prenom = Console.ReadLine();

            //Avant d'ajouter cet intervenant, on vérifie s'il n'existe pas déjà dans la liste
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
                //Si l'intervenant n'existe pas déjà dans la liste,
                //On demande à l'utilisateur d'indiquer qui est cet intervenant (3 choix possibles)
                //L'utilisteur devra ensuite entrer des caractéristiques propres au type d'intervenant sélectionné
                Console.WriteLine("L'intervenant est un : \n 1 - Elève de l'ENSC \n 2 - Enseignant de l'ENSC \n 3 - Extérieur à l'ENSC");
                int TypeIntervenant = Convert.ToInt32(Console.ReadLine());
                if (TypeIntervenant == 1) //Choix de la promotion pour un élève
                {
                    Console.Write("Promotion de l'élève (4 chiffres) : ");
                    int Promo = Convert.ToInt32(Console.ReadLine());
                    Eleve nouvelIntervenant = new Eleve(Nom, Prenom, Promo);
                    ListeIntervenants.Add(nouvelIntervenant);
                    nvListeIntervenant.Add(ListeIntervenants[ListeIntervenants.IndexOf(nouvelIntervenant)]);
                    Console.WriteLine("\tL'intervenant a bien été ajouté au projet !");
                }
                if (TypeIntervenant == 2) //Choix des matières enseignées pour un professeur
                {
                    //Affichage de la liste des matières existantes
                    List<Matiere> listmat = new List<Matiere> { };
                    Console.WriteLine("Voici la liste des matières possibles : ");
                    int k = 1;
                    foreach (Matiere m in ListeMatieres)
                    {
                        Console.WriteLine("{0} - {1}", k, m.ToString());
                        k++;
                    }
                    int choixMatiere = 1;

                    //Sélection des matières
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
                        //Vérification si la matière a déjà été sélectionnée pour ce projet
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
                if (TypeIntervenant == 3) //Choix du métier pour une personne extérieure à l'ENSC 
                {
                    Console.Write("Métier : ");
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
            //L'utilisateur entre le Nom et le Prénom de l'intervenant à rechercher
            Console.WriteLine("Rechercher d'intervenants");
            Console.Write("Nom : ");
            string NomRecherche = Console.ReadLine();
            Console.Write("Prénom : ");
            string PrenomRecherche = Console.ReadLine();

            //Initialisation de la liste Resultat qui contiendra tous les intervenants
            //correspondants à la recherche de l'utilisateur
            List<Intervenant> Resultat = new List<Intervenant>();

            //Parcours de la liste des intervenants existants dans le catalogue
            //et évaluation de leur correspondance avec la recherche de l'utilisateur
            foreach (Intervenant intervenant in ListeIntervenants)
            {
                if (intervenant.Nom.IndexOf(NomRecherche) != -1 || intervenant.Prenom.IndexOf(PrenomRecherche) != -1)
                    Resultat.Add(intervenant);
            }

            int j = 0;
            //Affichage des résultats de recherche
            foreach (Intervenant i in Resultat)
            {
                j++;
                Console.WriteLine("{0} - {1}", j, i.ToString());
            }
            if (j == 0)
                Console.WriteLine("Aucun résultat pour cette recherche.");
            else
            {
                //Sélection de l'intervenant à ajouter parmi les résultats de la recherche
                Console.Write("Ajouter l'intervenant (entrez 0 pour annuler) : ");
                int choixIntervenant = Convert.ToInt32(Console.ReadLine());
                //Vérification : le numéro entré par l'utilisateur doit correspondre à un choix possible
                while (choixIntervenant < 0 || choixIntervenant > j)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("\nAjouter l'intervenant (entrez 0 pour annuler) : ");
                    choixIntervenant = Convert.ToInt32(Console.ReadLine());
                }

                //Vérification : l'intervenant ne doit pas être ajouté plusieurs fois
                if (choixIntervenant != 0)
                {
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
                        //Ajout de l'intervenant à la liste des intervenants du projet
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
            //Affichage de tous les intervenants du catalogue
            foreach(Intervenant i in ListeIntervenants)
            {
                j++;
                Console.WriteLine("{0} - {1}", j, i.ToString());
            }

            Console.Write("\nVous pouvez ajouter plusieurs intervenants. Tapez 0 pour finir");
            int choixIntervenant = 1;
            while (choixIntervenant != 0)
            {
                //Sélection de l'intervenant à ajouter
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
                        //Ajout de l'intervenant à la liste des intervenants du projet
                        nvListeIntervenant.Add(ListeIntervenants[choixIntervenant - 1]);
                        Console.WriteLine("\tL'intervenant a bien été ajouté !");
                    }
                }
            }
        }



        public List<Role> ChoixRole(List<Intervenant> listeIntervenant)
        {
            //Création de la liste des rôles associée aux intervenants du projet : nvListeRoles
            //On affiche la liste des rôles possibles : ListeRoles
            //L'utilisateur sélectionne les rôles qu'il souhaite attribuer à chaque intervenant
       
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
            //Renvoi de la liste des rôles des intervenants du projet
            return nvListeRole;
        }


        public List<Matiere> ChoixMatiere()
        {
            //Création de la liste des matières associée au projet : nvListeMatieres
            //On affiche la liste des matières possibles : ListeMatieres
            //L'utilisateur sélectionne les matières qu'il souhaite attribuer au projet
            //Lorsqu'il a terminé, il tape 0 pour sortir de la boucle

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

                //Vérification : l'utilisateur doit entrer un numéro qui correspond à un choix possible
                while (choixMatiere < 0 || choixMatiere > j - 1)
                {
                    Console.WriteLine("Je n'ai pas compris votre choix");
                    Console.Write("Ajouter une matière (entrez 0 pour finir) : ");
                    choixMatiere = Convert.ToInt32(Console.ReadLine());
                }

                //Vérification : l'utilisateur ne doit pas entrer plusieurs fois la même matière
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
                        //Ajout de la matière à la liste des matières du projet
                        nvListeMatieres.Add(ListeMatieres[choixMatiere - 1]);
                        Console.WriteLine("\tLa matière a bien été ajoutée");
                    }
                }
            }
            //Renvoie la liste des matières associées au projet
            return nvListeMatieres;
        }


        public List<Livrable> ChoixLivrable()
        {
            //Création de la liste des livrables associée au projet : nvListeLivrable
            //On affiche la liste des livrables possibles : ListeLivrables
            //L'utilisateur sélectionne les livrables qu'il souhaite attribuer au projet
            //Lorsqu'il a terminé, il tape 0 pour sortir de la boucle

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

                //Vérification : l'utilisateur doit entrer un numéro qui correspond à un choix possible
                // >j car il y a une option en plus (ajouter un livrable)
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
                        //Ajout du livrable à la liste des livrables existants
                        ListeLivrables.Add(nouveauLivrable);
                        //Ajout du livrable à la liste des livrables associés au projet
                        nvListeLivrable.Add(ListeLivrables[choixLivrable - 1]);
                        Console.WriteLine("\tLe livrable a bien été ajouté");
                    }
                    else
                    {
                        // Vérification : l'utilisateur ne doit pas entrer plusieurs fois le même livrable
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
                            //Ajout du livrable à la liste des livrables associés au projet
                            nvListeLivrable.Add(ListeLivrables[choixLivrable - 1]);
                            Console.WriteLine("\tLe livrable a bien été ajouté");
                        }
                    }
                }
            }
            //Renvoie la liste des livrables associés au projet
            return nvListeLivrable;
        }

        



        public void SupprimerProjet()
        {
            //Affichage de tous les projets du catalogue
            Console.WriteLine("Voici la liste des projets : ");
            int j = 1;
            foreach (Projet projet in ListeProjets)
            {
                Console.WriteLine("{0} - {1}\n", j, projet.ToString());
                j++;
            }

            //Sélection du projet à supprimer
            Console.Write("\nLequel voulez-vous supprimer ?  ");
            int choixSupression = Convert.ToInt32(Console.ReadLine());
            while (choixSupression < 0 || choixSupression > j-1)
            {
                //Vérification : le numéro entré par l'utilisateur doit correspondre à un des projets
                Console.Write("Je n'ai pas compris votre choix");
                Console.Write("\nLequel voulez-vous supprimer ?  ");
                choixSupression = Convert.ToInt32(Console.ReadLine());
            }
            //Suppression du projet
            ListeProjets.RemoveAt(choixSupression - 1);

            Console.WriteLine("Le projet a bien été supprimé !");
        }


        public void ModifierProjet()
        {
            //Affichage de tous les projets existants dans le catalogue
            Console.WriteLine("Voici la liste des projets : ");
            int j = 1;
            foreach (Projet projet in ListeProjets)
            {
                Console.WriteLine("{0} - {1}\n", j, projet.ToString());
                j++;
            }

            //Sélection du projet à modifier
            Console.Write("\nLequel voulez-vous modifier ?  ");
            int choixProjet = Convert.ToInt32(Console.ReadLine());
            while (choixProjet < 0 || choixProjet > j - 1)
            {
                //Vérification du choix utilisateur
                Console.Write("Je n'ai pas compris votre choix");
                Console.Write("\nLequel voulez-vous modifier ?  ");
                choixProjet = Convert.ToInt32(Console.ReadLine());
            }
            List<string> listeElements = new List<string> { "type de projet", "thème", "sujet libre", "dates", "intervenants et rôles", "matieres", "livrables"};
            int choixModif = 1;
            while(choixModif !=0)
            {
                //Choix du paramètre à modifier
                foreach (string e in listeElements)
                {
                    Console.WriteLine("{0} - {1}", listeElements.IndexOf(e) + 1, e.ToString());
                }
                Console.Write("\nQue voulez-vous modifier (tapez 0 pour finir) ?  ");
                choixModif = Convert.ToInt32(Console.ReadLine());
                while (choixModif < 0 || choixModif > listeElements.Count)
                {
                    //Vérification du choix utilisateur
                    Console.Write("Je n'ai pas compris votre choix");
                    Console.Write("\nLequel voulez-vous modifier ?  ");
                    choixModif = Convert.ToInt32(Console.ReadLine());
                }

                //Modification du type de projet
                if (choixModif == 1)
                {
                    ListeProjets[choixProjet - 1].TypeProjet = choixTypeProjet();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                //Modification du thème
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
                //Modification du sujet libre ou imposé
                if (choixModif == 3)
                {
                    ListeProjets[choixProjet - 1].SujetLibre = choixSujetLibre();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                //Modification des dates de début et de fin de projet
                if (choixModif == 4)
                {
                    DateTime[] tabDates = choixDates();
                    ListeProjets[choixProjet - 1].DateDebut = tabDates[0];
                    ListeProjets[choixProjet - 1].DateFin = tabDates[1];
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                //Modification des intervenants et de leurs rôles
                if (choixModif == 5)
                {
                    List<Intervenant> listeInterv = ChoixIntervenant();
                    ListeProjets[choixProjet - 1].IntervenantsConcernes = listeInterv;
                    ListeProjets[choixProjet - 1].RolesIntervenants = ChoixRole(listeInterv);
                    ListeProjets[choixProjet - 1].NbIntervenants = listeInterv.Count;
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                //Modification des matières 
                if (choixModif == 6)
                {
                    ListeProjets[choixProjet - 1].MatieresConcernees = ChoixMatiere();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
                //Modification des livrables
                if (choixModif == 7)
                {
                    ListeProjets[choixProjet - 1].LivrablesAttendus = ChoixLivrable();
                    Console.WriteLine("Les modifications ont bien été prises en compte !\n");
                }
            }
        }


        public void Sauvegarder(string fichierXML)
        {
            //conversion des objets c# en données compréhensibles par un fichier xml
            XmlSerializer serializer = new XmlSerializer(typeof(Catalogue));
            //écriture des données dans le fichier xml
            TextWriter writer = new StreamWriter(fichierXML);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        public void ReinitialiserCatalogue()
        {
            //On supprime le catalogue utilisateur
            File.Delete("Users\\juliettesquirol\\Documents\\GitHub\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml");
            //File.Delete("Users\\leagr\\Source\\Repos\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml");

            //On copie le catalogue de base, et on le colle en créant un nouveau catalogue utilisateur
            string sourceFile = "Users\\juliettesquirol\\Documents\\GitHub\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\catalogueBase.xml";
            //string sourceFile = "Users\\leagr\\Source\\Repos\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\catalogueBase.xml";

            string destinationFile = "Users\\juliettesquirol\\Documents\\GitHub\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml";
            //string destinationFile = "Users\\leagr\\Source\\Repos\\projetClasseENSC\\ProjetCeption\\bin\\Debug\\netcoreapp3.0\\sauvegardeCatalogue.xml";

            File.Copy(sourceFile, destinationFile);

        }

    }
}