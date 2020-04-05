using System;
namespace ProjetCeption
{
    public class Matiere
    {

        public string nomMatiere { get; set; }
        public string codeSyllabus { get; set; }

        public Matiere(string matiere, string codeSyll)
        {
            if (VerifMatiere(matiere) == true)
            {
                nomMatiere = matiere;
                codeSyllabus = codeSyll;
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

            if (matiere == "programmation avancée" || matiere == "programmation web" || matiere == "GESP")
            {
                matiereExistente = true;
            }
            else
            {
                matiereExistente = false;
            }

            return matiereExistente;

        }

    }
}
