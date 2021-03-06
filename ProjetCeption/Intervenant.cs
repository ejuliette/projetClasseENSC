﻿using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public abstract class Intervenant
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
       
        public Intervenant() { }

        public Intervenant(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }

        //Affichage des caractéristiques de l'objet Intervenant
        public override string ToString()
        {
            return "Nom : " + Nom + "\nPrénom : " + Prenom ;
        }

    }
}



