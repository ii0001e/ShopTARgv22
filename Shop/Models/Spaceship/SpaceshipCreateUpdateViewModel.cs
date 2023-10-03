namespace Shop.Models.Spaceship
{
    public class SpaceshipCreateUpdateViewModel
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Passangers { get; set; }

        public int EnginPower { get; set; }

        public int Crew { get; set; }

        public string Company { get; set; }

        public int CargoWeight { get; set; }

        public List<IFormFile> Files { get; set; }

        public List<FileToApiViewModel> FileToApiViewModels { get; set; }
        = new List<FileToApiViewModel>();



        //only in database

        public DateTime CreatedAt { get; set; }

        public DateTime Modifieted { get; set; }
    }
}
