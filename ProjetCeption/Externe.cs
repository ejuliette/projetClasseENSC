using System;
namespace ProjetCeption
{
    public class Externe : Intervenant
    {
        public string Metier { get; set; }

        public Externe() : base() { }

        public Externe(string n, string p, string metier) : base(n, p)
        {
            Metier = metier;
        }


        public override string ToString()
        {
            string description = base.ToString() + "\nMétier : " + Metier + "\n";
            return description;
        }
    }
}



