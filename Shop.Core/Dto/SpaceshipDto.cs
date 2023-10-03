using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Dto
{
    public class SpaceshipDto
    {
        public Guid? Id { get; set; }

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
          //works with img 
        public List<IFormFile> Files { get; set; }

        public IEnumerable<FileToApiDto> Image { get; set; }

        = new List<FileToApiDto>();

    }
}
