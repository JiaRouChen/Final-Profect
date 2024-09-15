using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace D05Project.Models;

public partial class OrderDetail
{
    [Display(Name = "訂單代號")]
    public string orderId { get; set; } = null!;

    [Display(Name = "產品代號")]
    public string pId { get; set; } = null!;

    [Display(Name = "數量")]
    public decimal quantity { get; set; }

    [Display(Name = "價格")]
    public decimal ProductPrice { get; set; }

    public virtual Orders? order { get; set; } 

    public virtual Product? pIdNavigation { get; set; }

    
}
