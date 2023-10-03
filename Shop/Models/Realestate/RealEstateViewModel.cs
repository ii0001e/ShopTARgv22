namespace Shop.Models.Realestate
{
    public class RealEstateViewModel
    {
        public Guid? Id { get; set; }
        public string Address { get; set; }
        public float SizeSqrM { get; set; }
        public int RoomCount { get; set; }

        public int Floor { get; set; }
     

       
    }
}
