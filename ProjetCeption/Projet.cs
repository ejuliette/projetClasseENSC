using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public class Projet
    {
        public int nbIntervenants { get; set; }
        public bool sujetLibre { get; set; }
        public DateTime dateDebut { get; set; }
        public DateTime dateFin { get; set; }
        public string theme { get; set; }
        public List<Livrable> livrablesAttendus { get; set; }
        public List<Matiere> matieresConcernees { get; set; }
        public List<Intervenant> intervenantsConcernes { get; set; }


        public Projet(int nbInt, bool sujetLib, string deb, string fin, string th, List<Livrable> livr, List<Matiere> mat, List<Intervenant> interv )
        {
            nbIntervenants = nbInt;
            sujetLibre = sujetLib;
            dateDebut = DateTime.Parse(deb);
            dateFin = DateTime.Parse(fin);
            theme = th;
            livrablesAttendus = livr;
            matieresConcernees = mat;
            intervenantsConcernes = interv;
        }

        public override string ToString()
        {
            string description = "\n Nombre d'intervenants : " + nbIntervenants +
                "\n Le sujet est libre : " + sujetLibre + "\n Date de début : " + dateDebut +
                "\n Date de fin : " + dateFin + "\n Thème : " + theme + "\n Livrables attendus : ";

            foreach (Livrable item in livrablesAttendus)
            {
                description = description + item.nomLivrable + " ";
            }

            description = description + "\n Matières concernées : ";

            foreach (Matiere item in matieresConcernees)
            {
                description = description + item.nomMatiere + " ";
            }

            description = description + "\n Intervenants concernés : ";

            foreach (Intervenant item in intervenantsConcernes)
            {
                description = description + item.Prenom + " " + item.Nom + " ";
            }


            return description;
        }
    }
}
