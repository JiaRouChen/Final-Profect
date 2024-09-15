using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D05Project.ViewModels;
using System.ComponentModel.DataAnnotations;
namespace D05Project.Models;

public partial class Product
{
    [Display(Name = "供應商編號")]
    public string sId { get; set; } = null!;
    [Display(Name = "產品編號")]
    public string pId { get; set; } = null!;
    [Display(Name = "產品規格")]
    public string unit { get; set; } = null!;
    [Display(Name = "安全庫存")]
    public decimal saveQuantity { get; set; }
    [Display(Name = "數量")]
    public decimal quantity { get; set; }
    [Display(Name = "產品名稱")]
    public string prodoctName { get; set; } = null!;
    [Display(Name = "產品種類")]
    public string catId { get; set; } = null!;
    [Display(Name = "產品價格")]
    public decimal prodoctPrice { get; set; }

    public virtual ICollection<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();

    public virtual ICollection<PurchaseRecord> PurchaseRecord { get; set; } = new List<PurchaseRecord>();

    public virtual Category? cat { get; set; } 

    public virtual Supplier? sIdNavigation { get; set; } 

    public virtual ICollection<withdrawalRecord> withdrawalRecord { get; set; } = new List<withdrawalRecord>();
}
