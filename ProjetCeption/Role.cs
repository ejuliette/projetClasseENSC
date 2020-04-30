using System;
namespace ProjetCeption
{
    public class Role
    {
        public string NomRole { get; set; }

        public Role(){ }
        public Role(string nomRole)
        {
           
        }

        public override string ToString()
        {
            return NomRole;
        }
    }
}



