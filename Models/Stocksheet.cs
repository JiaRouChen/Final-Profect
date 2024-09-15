using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace D05Project.Models;

public partial class Stocksheet
{
    [Display(Name = "入庫編號")]
    public string sNo { get; set; } = null!;

    [Display(Name = "入庫日期")]
    public DateOnly sDate { get; set; }

    [Display(Name = "員工編號")]
    public string aId { get; set; } = null!;
    [Display(Name = "入庫備註")]
    public string? sNote { get; set; }

    public virtual ICollection<PurchaseRecord> PurchaseRecord { get; set; } = new List<PurchaseRecord>();

    public virtual Admin? aIdNavigation { get; set; }
}
