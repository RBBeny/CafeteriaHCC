using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaHCC.Models
{
    [Table("Tb_HccOrdenDetalle")]
    public class OrdenesDetalle
    {
        [Key]
        [Column("orddet_id")]
        public int Id { get; set; }

        [Column("ord_id")]
        public int OrdenId { get; set; }

        [ForeignKey("OrdenId")]
        public Ordenes Ordenes { get; set; }

        [Column("pro_id")]
        public int ProductoId { get; set; }

        [ForeignKey("ProductoId")]
        public Producto Producto { get; set; }

        [Column("orddet_cantidad")]
        public short Cantidad { get; set; }

        [Column("orddet_estatus")]
        public byte Estatus { get; set; }
    }
}