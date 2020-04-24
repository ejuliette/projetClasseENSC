



using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public class Catalogue
    {
        public List<Matiere> ListeMatieres { get; set; }
        public List<Eleve> ListeEleves { get; set; }
        public List<Enseignant> ListeEnseignants { get; set; }
        public List<Externe> ListeExternes { get; set; }
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

        public void RechercherParAnnee(int annee)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in ListeProjets)
            {
                if (projet.DateDebut.Year == annee || projet.DateFin.Year == annee)
                {
                    Resultat.Add(projet);
                    Console.WriteLine(projet.ToString());
                }
            }
        }

        public List<Projet> RechercherParIntervenant(string nom, string prenom)
        {
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
            return Resultat;
        }


    }
}



