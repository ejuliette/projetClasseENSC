﻿using System;
namespace ProjetCeption
{
    public class Eleve : Intervenant
    {
        public int Promo { get; set; }

        public Eleve(string n, string p, int promo) : base(n, p)
        {
            Promo = promo;
        }

        public override string ToString()
        {
            return base.ToString() + "\nLa promo de l'élève est:" + Promo;
        }
    }
}



