using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D05Project.Models;

public partial class Member
{
    [Display(Name = "帳號")]
    public string MemId { get; set; } = null!;
    [Display(Name = "會員姓名")]
    public string MemName { get; set; } = null!;
    [Display(Name = "密碼")]
    public decimal MemTel { get; set; }
    [Display(Name = "會員信箱")]
    public string MemEmail { get; set; } = null!;
    [Display(Name = "會員生日(西元)")]
    public DateOnly MemBirth { get; set; }
    [Display(Name = "會員地址")]
    public string Memaddress { get; set; } = null!;
    [Display(Name = "性別")]
    public bool Memgender { get; set; }

    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
