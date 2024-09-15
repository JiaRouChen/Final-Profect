using System;
using System.Collections.Generic;

namespace D05Project.Models;
using System.ComponentModel.DataAnnotations;
public partial class PurchaseRecord
{
    [Display(Name = "供應商代碼")]
    public string sId { get; set; } = null!;
    [Display(Name = "產品代碼")]
    public string pId { get; set; } = null!;
    [Display(Name = "入庫編號")]
    public string sNo { get; set; } = null!;
    [Display(Name = "數量")]
    public decimal quantity { get; set; }

    public virtual Product? pIdNavigation { get; set; } 

    public virtual Supplier? sIdNavigation { get; set; } 

    public virtual Stocksheet? sNoNavigation { get; set; } 
}
