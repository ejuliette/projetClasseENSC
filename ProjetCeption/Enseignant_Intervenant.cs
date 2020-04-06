using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public class Enseignant : Intervenant
    {

        public List<Matiere> Matieres { get; set; }
       


        public Enseignant(string n, string p, List<Matiere> matieres) : base(n, p)
        {
            this.Matieres = matieres;
         
        }

        public override string ToString()
        {
            string description = base.ToString() + "\nLes matières de cet enseignant sont : " ;

            foreach (Matiere item in Matieres)
            {
                description = description + item.nomMatiere + " ";
            }


            return description;
        }

    }
}
