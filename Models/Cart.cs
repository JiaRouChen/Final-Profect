
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D05Project.Models;

public partial class Cart
{
    [Display(Name = "姓名")]
    public string cartId { get; set; } = null!;
    [Display(Name = "選擇產品")]
    public string pId { get; set; } = null!;
    [Display(Name = "付款方式")]
    public string orderId { get; set; } = null!;
    [Display(Name = "產品數量")]
    public decimal productqty { get; set; }
    [Display(Name = "連絡電話")]
    public decimal productprice { get; set; }
    [Display(Name = "地址")]
    public string productName { get; set; } = null!;
    [Display(Name = "產品規格")]
    public string MemId { get; set; } = null!;
}
