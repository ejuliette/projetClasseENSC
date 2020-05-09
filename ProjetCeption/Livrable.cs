using System;
namespace ProjetCeption
{
    public class Livrable
    {
        public string NomLivrable { get; set; }

        public Livrable() { }
        public Livrable(string livrable)
        {
            NomLivrable = livrable;
        }

        //Affichage des caractéristiques de l'objet Livrable
        public override string ToString()
        {
            string description = NomLivrable ;
            return description;
        }
    }
}


