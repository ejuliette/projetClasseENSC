using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public class Projet
    {
        public string Theme { get; set; }
        public string TypeProjet { get; set; }
        public bool SujetLibre { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public int NbIntervenants { get; set; }
        public List<Intervenant> IntervenantsConcernes { get; set; }
        public List<Matiere> MatieresConcernees { get; set; }
        public List<Livrable> LivrablesAttendus { get; set; }
        public List<Role> RolesIntervenants { get; set; }


        public Projet() { }
        public Projet(string type, string theme, bool sujetLibre, DateTime debut, DateTime fin, List<Intervenant> intervenants, List<Role> roles, List<Matiere> matieres, List<Livrable> livrables)
        {
            TypeProjet = type;
            Theme = theme;
            SujetLibre = sujetLibre;
            DateDebut = debut.Date;
            DateFin = fin.Date;
            NbIntervenants = intervenants.Count;
            IntervenantsConcernes = intervenants;
            MatieresConcernees = matieres;
            LivrablesAttendus = livrables;
            RolesIntervenants = roles;
        }

        //Affichage des caractéristiques de l'objet Projet
        public override string ToString()
        {
            string SujetLib = "Imposé";
            if (SujetLibre == true)
            {
                SujetLib = "Libre";
            }
            string description = "\n Thème : " + Theme + "\n Type de projet : " + TypeProjet +
                "\n Sujet : " + SujetLib + "\n Date de début : " + DateDebut.ToString("d") +
                "\n Date de fin : " + DateFin.ToString("d") + "\n Nombre d'intervenants : " + NbIntervenants ;

            description = description + "\n Intervenants concernés : ";
            foreach (Intervenant item in IntervenantsConcernes)
            {
                int i = IntervenantsConcernes.IndexOf(item);
                description += "\n\t" + item.Prenom + " " + item.Nom + ": ";
                description += RolesIntervenants[i].ToString();
            }

            description = description + "\n Matières concernées : ";
            foreach (Matiere item in MatieresConcernees)
            {
                description = description + item.NomMatiere + ", ";
            }

            description = description + "\n Livrables attendus : ";
            foreach (Livrable item in LivrablesAttendus)
            {
                description = description + item.NomLivrable + ", ";
            }

            return description;
        }
    }
}



