using System;
namespace ProjetCeption
{
    public class Role
    {
        public string role { get; set; }
      
        public Role(string roleIntervenant)
        {
            role = roleIntervenant;
        }

        public override string ToString()
        {
            return "\nLe rôle de cet intervenant est :" + this.role;
        }
    }
}
