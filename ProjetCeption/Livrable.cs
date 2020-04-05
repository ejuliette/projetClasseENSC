using System;
namespace ProjetCeption
{
    public class Livrable
    {
        public string nomLivrable { get; set; }

        public Livrable(string livrable)
        {
            if(VerifLivrable(livrable) == true)
            {
                nomLivrable = livrable;
            }
            else
            {
                Console.WriteLine("Ce livrable n'est pas accepté.");
                //Ce serait pas mal d'afficher tous les livrables acceptés
            }

        }

        //Je pense qu'on devrait faire une liste ou un tableau
        //qui contiendrait tout les types de livrables et dans laquelle
        //on pourrait ajouter de nouveaux types de livrables au besoin
        //mais je ne sais pas où il est judicieux de la placer

        public bool VerifLivrable(string livrable)
        {
            bool livrableExistant;

            if(livrable=="analyse de l'existant" || livrable=="site web")
            {
                livrableExistant = true;
            }
            else
            {
                livrableExistant = false;
            }

            return livrableExistant;

        }
    }
}
