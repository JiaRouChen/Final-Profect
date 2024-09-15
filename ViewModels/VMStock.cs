using D05Project.Models;

namespace D05Project.ViewModels
{
    public class VMStock
    {
  
        public List<Product>? Product { get; set; }
        public List<withdrawalRecord>? withdrawalRecord { get; set; }

        public List<PurchaseRecord>? PurchaseRecord { get; set; }
    }
}
