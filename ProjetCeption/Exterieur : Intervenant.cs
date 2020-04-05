using System;
namespace ProjetCeption
{
    public class Exterieur : Intervenant
    {
        private string Metier { get; set; }


        public Exterieur(string n, string p, string metier) : base(n, p)
        {
            this.Metier = metier;

        }


        
        public override string ToString()
        {
            return base.ToString() + "\nLe métier de cet intervenant extérieur est : " + this.Metier;
        }
    }
}
