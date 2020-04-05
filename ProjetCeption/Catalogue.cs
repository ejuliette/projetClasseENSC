using System;
using System.Collections.Generic;

namespace ProjetCeption
{
    public class Catalogue
    {
      
        public List<Projet> ListeProjets { get; set; }

       
        public Catalogue()
        {

        }

        public void Annee(int annee)
        {
            Console.WriteLine(annee); 
        }

        public void Promo(int promo)
        {
            Console.WriteLine(promo);
        }

        public void MotCle(string motCle)
        {
            Console.WriteLine(motCle);
        }

        public void Eleve(string nom, string prenom)
        {
            Console.WriteLine(nom, prenom);
        }
    }
}
