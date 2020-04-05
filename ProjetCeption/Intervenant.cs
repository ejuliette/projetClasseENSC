using System;
namespace ProjetCeption
{
    public abstract class Intervenant
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Intervenant(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
           
        }

        public override string ToString()
        {
            return "\nLe nom est : " + this.Nom + "\nLe prénom est : " + this.Prenom + "\n";  
        }

    }
}
