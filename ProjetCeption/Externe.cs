using System;
namespace ProjetCeption
{
    public class Externe : Intervenant
    {
        private string Metier { get; set; }


        public Externe(string n, string p, string metier) : base(n, p)
        {
            Metier = metier;
        }



        public override string ToString()
        {
            return base.ToString() + "\nLe métier de cet intervenant extérieur est : " + Metier;
        }
    }
}



