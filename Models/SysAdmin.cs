using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D05Project.Models;

public partial class SysAdmin
{
    [Display(Name = "姓名")]
    public string sysId { get; set; } = null!;
    [Display(Name = "居住縣市")]
    public string sysname { get; set; } = null!;
    [Display(Name = " ")]
    public bool sysgender { get; set; }
    [Display(Name = "下單日")]
    public DateOnly sysbirthday { get; set; }
    [Display(Name = "聯絡電話")]
    public string syscId { get; set; } = null!;
    [Display(Name = "地址(無縣市)")]
    public string syspassword { get; set; } = null!;

    public virtual ICollection<Admin> Admin { get; set; } = new List<Admin>();
    public SysAdmin()
    {
        sysbirthday = DateOnly.FromDateTime(DateTime.Now);
    }
}
