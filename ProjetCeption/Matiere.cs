using System;
using System.ComponentModel;

namespace ProjetCeption
{
    public class Matiere
    {
        public string NomMatiere { get; set; }
        public string CodeSyllabus { get; set; }

        public Matiere() { }

        public Matiere(string matiere, string codeSyll)
        {
            NomMatiere = matiere;
            CodeSyllabus = codeSyll;
        }


        //Affichage des caractéristiques de l'objet Matiere
        public override string ToString()
        {
            string description = NomMatiere + ", " + CodeSyllabus ;
            return description ;
        }

    }
}



