using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace D05Project.Models;

public partial class ProcurmentSheet
{
    [Display(Name = "出庫編號")]
    public string pNo { get; set; } = null!;
    [Display(Name = "出庫日")]
    public DateOnly pDate { get; set; }
    [Display(Name = "出處理員工")]
    public string aId { get; set; } = null!;
    [Display(Name = "出庫備註")]
    public string? pNote { get; set; }

    public virtual Admin? aIdNavigation { get; set; } 

    public virtual ICollection<withdrawalRecord> withdrawalRecord { get; set; } = new List<withdrawalRecord>();
}
