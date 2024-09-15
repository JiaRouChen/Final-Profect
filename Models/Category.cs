using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace D05Project.Models;

public partial class Category
{
    [Display(Name = "種類代號")]
    public string catId { get; set; } = null!;
    [Display(Name = "種類名稱")]
    public string catName { get; set; } = null!;

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
