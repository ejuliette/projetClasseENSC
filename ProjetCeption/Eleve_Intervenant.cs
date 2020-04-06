using System;
namespace ProjetCeption
{
    public class Eleve : Intervenant
    {
        private int Promo { get; set; }
        private int Annee { get; set; }

        public Eleve(string n, string p, int promo, int annee) : base(n,p)
        {
            this.Promo = promo;
            this.Annee = annee;
        }

        public override string ToString()
        {
            return base.ToString() + "\nLa promo de l'élève est:" + this.Promo + "\nL'année est :" + this.Annee;
        }
    }
}
