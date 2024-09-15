using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace D05Project.Models;

public partial class ReBook
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)] // Assuming GId is manually assigned
    public long RId { get; set; }

    public long GId { get; set; }

    [StringLength(30)]
    [Display(Name = "描述")]
    public string? Description { get; set; }
    [Display(Name = "作者")]
    [StringLength(10)] // Changed from 20 to match your SQL schema
    public string? Author { get; set; }

    public DateTime? TimeStamp { get; set; } // Nullable if it can be NULL

    


}
    
