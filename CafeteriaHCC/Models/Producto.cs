using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaHCC.Models
{
    [Table("Tb_Producto")]
    public class Producto
    {
        [Key]
        [Column("pro_id")]
        public int Id { get; set; }

        [Column("alm_id")]
        public int AlmacenId { get; set; }

        [ForeignKey("AlmacenId")]
        public Almacen Almacen { get; set; }

        [Column("pro_nombre")]
        public string Nombre { get; set; }

        [Column("pro_descripcion")]
        public string Descripcion { get; set; }

        [Column("pro_precio", TypeName = "decimal(10,4)")]
        public decimal Precio { get; set; }

        [Column("pro_estatus")]
        public byte Estatus { get; set; }
    }
}