using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace D05Project.Models;

public partial class Book
{

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Assuming GId is manually assigned
        public long GId { get; set; }
    [Display(Name = "標題")]
    [StringLength(30)]
        public string? Title { get; set; }
    [Display(Name = "描述")]
    public string? Description { get; set; }
    [Display(Name = "作者")]
    [StringLength(20)]
        public string? Author { get; set; }
    [Display(Name = "時間")]
    public DateTime? TimeStamp { get; set; } // Nullable if it can be NULL

    public byte[]? Photo { get; set; } // VARBINARY(MAX) maps to byte[]

        public string? ImageType { get; set; }
    
}