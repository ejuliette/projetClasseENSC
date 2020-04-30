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
        public List<Matiere> ListeMatieres { get; set; }
        public List<Eleve> ListeEleves { get; set; }
        public List<Enseignant> ListeEnseignants { get; set; }
        public List<Externe> ListeExternes { get; set; }
        public List<Intervenant> ListeIntervenants { get; set; }
        public List<Livrable> ListeLivrables { get; set; }
        public List<Projet> ListeProjets { get; set; }
        public List<Role> ListeRoles { get; set; }

        public Catalogue()
        {
            /*
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
            ListeRoles = new List<Role> { Tuteur, Cobaye, Acteur };

            List<Livrable> livrablesProjet1 = new List<Livrable> { siteWeb, rapport };
            List<Matiere> matieresProjet1 = new List<Matiere> { ProgAv, Gesp };
            List<Intervenant> intervenantsProjet1 = new List<Intervenant> { Juliette, Milo, Pesquet };
            List<Role> rolesIntervenantsProjet1 = new List<Role> { Acteur, Cobaye, Tuteur };
            Projet Projet1 = new Projet("Transdi", "Projet 1", true, DateTime.Parse("01/08/2015"), DateTime.Parse("01/09/2015"), intervenantsProjet1.Count, intervenantsProjet1, matieresProjet1, livrablesProjet1, rolesIntervenantsProjet1);
            //Si on fait comme ça, qu'est ce qu'on met dans AssocierRole ??
            Projet1.AssocierRoleIntervenant(Acteur, Juliette);
            Projet1.AssocierRoleIntervenant(Cobaye, Milo);
            Projet1.AssocierRoleIntervenant(Tuteur, Pesquet);



            List<Livrable> livrablesProjet2 = new List<Livrable> { analyseExistant, rapport };
            List<Matiere> matieresProjet2 = new List<Matiere> { Signal };
            List<Intervenant> intervenantsProjet2 = new List<Intervenant> { Léa, Hippo, Lanusse };
            List<Role> rolesIntervenantsProjet2 = new List<Role> { Acteur, Acteur, Tuteur };
            Projet Projet2 = new Projet("Transpromo", "Projet 2", true, DateTime.Parse("01/09/2019"), DateTime.Parse("30/05/2020"), intervenantsProjet2.Count, intervenantsProjet2, matieresProjet2, livrablesProjet2, rolesIntervenantsProjet2);

            ListeProjets = new List<Projet> { Projet1, Projet2 };
            */

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
            Console.Write("\nType de projet : ");
            string nvType = Console.ReadLine();
            while(nvType=="")
            {
                Console.WriteLine("Vous devez remplir le champ");
                Console.Write("Type de projet : ");
                nvType = Console.ReadLine();
            }

            Console.Write("Thème : ");
            string nvTheme = Console.ReadLine();
            while (nvTheme == "")
            {
                Console.WriteLine("Vous devez remplir le champ");
                Console.Write("Thème : ");
                nvTheme = Console.ReadLine();
            }


            bool nvSujetLibre;
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


            //DateTime.Parse permet de vérifier si le string entré par l'utilisateur est bien une date. Si ça l'est, il le met dans dateDebutValide
            Console.Write("Date de début (jj/mm/aaaa) : "); 
            DateTime dateDebutValide;
            bool resultDateDebut = DateTime.TryParse(Console.ReadLine(), out dateDebutValide);
            while(resultDateDebut==false)
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

            List<Intervenant> nvListeIntervenant = ChoixIntervenant();
            List<Role> nvListeRole = ChoixRole(nvListeIntervenant);
            List<Matiere> nvListeMatiere = ChoixMatiere();
            List<Livrable> nvListeLivrable = ChoixLivrable();
            List<Role> nvListeRole = ChoixRole();

            Projet nouveauProjet = new Projet(nvType, nvTheme, nvSujetLibre, dateDebutValide, dateFinValide, nvListeIntervenant.Count, nvListeIntervenant, nvListeMatiere, nvListeLivrable, nvListeRole);
            ListeProjets.Add(nouveauProjet);
            Console.WriteLine("\nLe projet a bien été ajouté !");
        }

        public List<Intervenant> ChoixIntervenant()
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