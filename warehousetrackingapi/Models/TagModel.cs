namespace warehousetrackingapi.Models
{
    public class TagModel
    {
        public string TagType { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public string Tag { get; set; }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Status { get; set; }

    }
    public class TagModelDTO
    {
        public string Tag { get; set; }

        public string Antenna { get; set; }
        public string Ipaddress { get; set; }
    }
}