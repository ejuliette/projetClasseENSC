using System;
namespace ProjetCeption
{
    public class Role
    {
        public string NomRole { get; set; }

        public Role()
        {
           
        }

        public override string ToString()
        {
            return "\nLe rôle cet intervenant est " + NomRole;
        }
    }
}



