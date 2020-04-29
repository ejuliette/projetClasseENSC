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
            if (VerifMatiere(matiere) == true)
            {
                NomMatiere = matiere;
                CodeSyllabus = codeSyll;
            }
            else
            {
                Console.WriteLine("Cette mantière n'est pas acceptée.");
                //Ce serait pas mal d'afficher toutes les matières acceptées
            }
        }

        



        public bool VerifMatiere(string matiere)
        {
            bool matiereExistente;

            if (matiere == "Programmation avancée" || matiere == "Programmation web" || matiere == "GESP" || matiere == "Signal")
            {
                matiereExistente = true;
            }
            else
            {
                matiereExistente = false;
            }

            return matiereExistente;
        }

        public override string ToString()
        {
            string description = NomMatiere + ", " + CodeSyllabus ;
            return description ;
        }

    }
}



