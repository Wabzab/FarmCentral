using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FarmCentral.Models;

[Table("Farmer")]
public partial class Farmer
{
    [Key]
    [Column("farmerID")]
    public int FarmerId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("password")]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    [InverseProperty("Farmer")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
