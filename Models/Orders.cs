using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace D05Project.Models;

public partial class Orders
{
    internal object Id;

    [Display(Name = "訂單代號")]
    public string orderId { get; set; } = null!;
    [Display(Name = "會員代號")]
    public string MemId { get; set; } = null!;
    [Display(Name = "處理員工代號")]
    public string aId { get; set; } = null!;
    [Display(Name = "下單日")]
    public DateTime orderDate { get; set; }
    [Display(Name = "預期取貨日")]
    public DateTime requiredDate { get; set; }
    [Display(Name = "出貨日")]
    public DateTime shippedDate { get; set; }
    [Display(Name = "運費")]
    public decimal Freight { get; set; }
    [Display(Name = "收件人姓名")]
    public string SName { get; set; } = null!;

    public virtual Member? Mem { get; set; } 

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    public virtual Admin? aIdNavigation { get; set; } 
}
