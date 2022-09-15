using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace warehousetrackingapi.Models
{
    

    public class StationModel : CreationModel
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(200)]
        public string Station { get; set; }

        public Guid Reader { get; set; }


    


        [Column(TypeName = "varchar")]
        [StringLength(10)]
        public string Active { get; set; }
    }

    public class StationModelDTO : CreationModel
    {

        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Station { get; set; }

        public string Reader { get; set; }


        public string Active { get; set; }
    }
}
