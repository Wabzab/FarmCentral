using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FarmCentral.Models;

[Table("Type")]
public partial class Type
{
    [Key]
    [Column("typeID")]
    public int TypeId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [InverseProperty("Type")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
