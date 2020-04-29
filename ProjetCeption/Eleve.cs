using System;
namespace ProjetCeption
{
    public class Eleve : Intervenant
    {
        public int Promo { get; set; }

        public Eleve() : base() { }

        public Eleve(string n, string p, int promo) : base(n, p)
        {
            Promo = promo;
        }


        public override string ToString()
        {
            string description = base.ToString() + "\nPromo : " + Promo +"\n";
            return description;
        }
    }
}



