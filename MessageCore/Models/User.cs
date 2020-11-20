using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageCore.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        
        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Name == user.Name &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Password);
        }
    }
}
