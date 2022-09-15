using warehousetrackingapi.Models;

namespace warehousetrackingapi.Models
{
    public class CatalogueDataModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int numberOfItems { get; set; }
        public List<CatalogueItemsModel> catalogueitems { get; set; }
    }
}
