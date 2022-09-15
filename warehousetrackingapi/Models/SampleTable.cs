using System.ComponentModel.DataAnnotations.Schema;

namespace TrackingDemoApi.Models
{
    public class SampleTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
