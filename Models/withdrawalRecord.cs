using System;
using System.Collections.Generic;

namespace D05Project.Models;
using System.ComponentModel.DataAnnotations;
public partial class withdrawalRecord
{
    [Display(Name = "供應商編號")]
    public string sId { get; set; } = null!;
    [Display(Name = "產品編號")]
    public string pId { get; set; } = null!;
    [Display(Name = "出庫編號")]
    public string pNo { get; set; } = null!;
    [Display(Name = "數量")]
    public decimal quantity { get; set; }

    public virtual Product? pIdNavigation { get; set; }

    public virtual ProcurmentSheet? pNoNavigation { get; set; }

    public virtual Supplier? sIdNavigation { get; set; } 
}
