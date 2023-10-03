using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Domain
{
    public class Spaceship
    {
        [Key]
        public  Guid? Id { get; set; }

        public string Name { get; set; }   

         public string Type { get; set; }

        public int Passangers { get; set; }

        public int EnginPower { get; set; }

        public int Crew { get; set; }

        public string Company { get; set; }

        public int CargoWeight { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Modifieted { get; set; }

        //only in database
    }
}
