using System;
namespace ProjetCeption
{
    public class Role
    {
        public string NomRole { get; set; }

        public Role(){ }
        public Role(string nomRole)
        {
            NomRole = nomRole;
        }

        //Affichage des caractéristiques de l'objet Role
        public override string ToString()
        {
            return NomRole;
        }
    }
}



