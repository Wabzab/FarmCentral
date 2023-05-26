using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FarmCentral.Models;

[Table("Product")]
public partial class Product
{
    [Key]
    [Column("productID")]
    public int ProductId { get; set; }

    [Column("farmerID")]
    public int FarmerId { get; set; }

    [Column("typeID")]
    public int TypeId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("date", TypeName = "date"), DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [ForeignKey("FarmerId")]
    [InverseProperty("Products")]
    public virtual Farmer Farmer { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("Products")]
    public virtual Type Type { get; set; } = null!;
}
