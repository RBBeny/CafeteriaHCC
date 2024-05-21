using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaHCC.Models
{
    [Table("Tb_HccCatEstatusOrden")]
    public class CatEstatusOrden
    {
        [Key]
        [Column("catord_id")]
        public int Id { get; set; }

        [Column("catord_nombre")]
        public string Nombre { get; set; }

        [Column("catord_estatus")]
        public byte Estatus { get; set; }
    }
}