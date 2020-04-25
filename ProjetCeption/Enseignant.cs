using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public class Enseignant : Intervenant
    {

        private List<Matiere> Matieres { get; set; }

        public Enseignant(string n, string p, List<Matiere> matieres) : base(n, p)
        {
            Matieres = matieres;
        }

        public override string ToString()
        {
            string description = base.ToString() + "\nMatières enseignées : ";

            foreach (Matiere item in Matieres)
            {
                description = description + item.NomMatiere + " ";
            }
            description += "\n";

            return description;
        }

    }
}



