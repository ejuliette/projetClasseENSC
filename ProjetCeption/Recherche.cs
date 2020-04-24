using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCeption
{
    class Recherche
    {

        public void RechercherParAnnee(Catalogue catalogue, int annee)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in catalogue.ListeProjets)
            {
                if (projet.DateDebut.Year == annee || projet.DateFin.Year == annee)
                {
                    Resultat.Add(projet);
                    Console.WriteLine(projet.ToString());
                }
            }
        }

        public List<Projet> RechercherParIntervenant(Catalogue catalogue, string nom, string prenom)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in catalogue.ListeProjets)
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

        public List<Projet> RechercherParPromo(Catalogue catalogue, int promo)
        {
            List<Projet> Resultat = new List<Projet>();
            foreach (Projet projet in catalogue.ListeProjets)
            {
                foreach (Eleve eleve in projet.IntervenantsConcernes)
                {
                    if (eleve.Promo == promo && eleve.Promo == promo)
                    {
                        Resultat.Add(projet);
                    }
                }
            }
            return Resultat;
        }



    }
}


