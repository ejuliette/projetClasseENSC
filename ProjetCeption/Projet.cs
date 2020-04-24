using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public class Projet
    {
        public string TypeProjet { get; set; }
        public string Theme { get; set; }
        public bool SujetLibre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        private int NbIntervenants { get; set; }
        public List<Intervenant> IntervenantsConcernes { get; set; }
        public List<Matiere> MatieresConcernees { get; set; }
        public List<Livrable> LivrablesAttendus { get; set; }


        public Projet(string type, string theme, bool sujetLibre, string debut, string fin, int nbInterv, List<Intervenant> intervenants, List<Matiere> matieres, List<Livrable> livrables)
        {
            TypeProjet = type;
            Theme = theme;
            SujetLibre = sujetLibre;
            DateDebut = DateTime.Parse(debut);
            DateFin = DateTime.Parse(fin);
            NbIntervenants = nbInterv;
            IntervenantsConcernes = intervenants;
            MatieresConcernees = matieres;
            LivrablesAttendus = livrables;
        }

        public void AssocierRoleIntervenant(Role role, Intervenant intervenant)
        {
            //On met quoi dedans ?
        }

        public override string ToString()
        {
            string description = "\n Nombre d'intervenants : " + NbIntervenants +
                "\n Le sujet est libre : " + SujetLibre + "\n Date de début : " + DateDebut +
                "\n Date de fin : " + DateFin + "\n Thème : " + Theme + "\n Livrables attendus : ";

            foreach (Livrable item in LivrablesAttendus)
            {
                description = description + item.NomLivrable + " ";
            }

            description = description + "\n Matières concernées : ";

            foreach (Matiere item in MatieresConcernees)
            {
                description = description + item.NomMatiere + " ";
            }

            description = description + "\n Intervenants concernés : ";

            foreach (Intervenant item in IntervenantsConcernes)
            {
                description = description + item.Prenom + " " + item.Nom + " ";
            }


            return description;
        }
    }
}



