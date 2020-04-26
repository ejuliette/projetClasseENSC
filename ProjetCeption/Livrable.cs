using System;
namespace ProjetCeption
{
    public class Livrable
    {
        public string NomLivrable { get; set; }

        public Livrable(string livrable)
        {
            NomLivrable = livrable;
        }

        public override string ToString()
        {
            string description = NomLivrable ;
            return description;
        }
    }
}


