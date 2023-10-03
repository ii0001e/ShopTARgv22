namespace Shop.Models.Realestate
{
    public class RealEstateDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }
        public float SizeSqrM { get; set; }
        public int RoomCount { get; set; }

        public int Floor { get; set; }
        public string BuildingType { get; set; }

        public DateTime BuiltinYear { get; set; } /*= DateTime.MinValue;*/


        public List<IFormFile> Files { get; set; }

        public List<FileToApiRealViewModel> FileToApiViewModels { get; set; }
        = new List<FileToApiRealViewModel>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
