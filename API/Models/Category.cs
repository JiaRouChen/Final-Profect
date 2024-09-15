using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductAPI.Models;

public partial class Category
{
    [Required(ErrorMessage = "公事種類代碼必填寫")]
    public string CatId { get; set; } = null!;
    [Required(ErrorMessage = "公事名稱必填寫")]
    public string? CatName { get; set; }
    [JsonIgnore]
    public virtual ICollection<sProduct>? sProduct { get; set; } = new List<sProduct>();
    //public virtual Category? Cat { get; set; }
}
