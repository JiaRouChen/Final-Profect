using System;
using System.Collections.Generic;

namespace D05Project.Models;
using System.ComponentModel.DataAnnotations;
public partial class Supplier
{
    [Display(Name = "供應商編號")]
    public string sId { get; set; } = null!;
    [Display(Name = "公司")]
    public string company { get; set; } = null!;
    [Display(Name = "地址")]
    public string address { get; set; } = null!;
    [Display(Name = "負責人")]
    public string Charge { get; set; } = null!;
    [Display(Name = "電話")]
    public decimal phone { get; set; }

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();

    public virtual ICollection<PurchaseRecord> PurchaseRecord { get; set; } = new List<PurchaseRecord>();

    public virtual ICollection<withdrawalRecord> withdrawalRecord { get; set; } = new List<withdrawalRecord>();
}
