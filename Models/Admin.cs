using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace D05Project.Models;

public partial class Admin
{
    [Display(Name = "員工代碼")]
    public string aId { get; set; } = null!;
    [Display(Name = "員工姓名")]
    public string Name { get; set; } = null!;
    [Display(Name = "員工性別")]
    public bool Gender { get; set; }
    [Display(Name = "員工生日")]
    public DateOnly Birthday { get; set; }
    [Display(Name = "員工身分證")]
    public string cId { get; set; } = null!;
    [Display(Name = "員工密碼")]
    public string Password { get; set; } = null!;
    [Display(Name = "上司代碼")]
    public string sysId { get; set; } = null!;

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();

    public virtual ICollection<ProcurmentSheet> ProcurmentSheet { get; set; } = new List<ProcurmentSheet>();

    public virtual ICollection<Stocksheet> Stocksheet { get; set; } = new List<Stocksheet>();

    public virtual SysAdmin? sys { get; set; } 
}
