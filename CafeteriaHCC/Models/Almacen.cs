using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CafeteriaHCC.Models
{
    [Table("Tb_HccAlmacen")]
    public class Almacen
    {
        [Key]
        [Column("alm_id")]
        public int Id { get; set; }

        [Column("alm_cantidad")]
        public int Cantidad { get; set; }

        [Column("alm_fecha_actualizacion")]
        public DateTime FechaActualizacion { get; set; }

        [Column("alm_estatus")]
        public byte Estatus { get; set; }
    }

}
