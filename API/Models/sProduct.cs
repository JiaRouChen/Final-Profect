using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProductAPI.Models;

public partial class sProduct
{
    public string PId { get; set; } = null!;

    public string? PName { get; set; }

    public int? QTY { get; set; }

    public string? CatId { get; set; }
    [JsonIgnore]
    public virtual Category? Cat { get; set; }
}
