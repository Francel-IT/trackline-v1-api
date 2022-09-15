using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    public class ReaderConfigModel 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }



        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Reader { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string IpAddress { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Hostname { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Antenna { get; set; }

        public Guid Location { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Action { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Active { get; set; }
    }

    public class ReaderConfigModelDTO
    {
        public string Reader { get; set; }

        public Guid Location { get; set; }

        public string Action { get; set; }

        public string LocationName { get; set; }

    }
}
