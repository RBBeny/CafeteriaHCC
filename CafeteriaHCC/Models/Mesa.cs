using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaHCC.Models
{
    [Table("Tb_HccMesas")]
    public class Mesa
    {
        [Key]
        [Column("mes_id")]
        public int Id { get; set; }

        [Column("mes_lugares")]
        public short Lugares { get; set; }

        [Column("mes_disponible")]
        public byte Disponible { get; set; }

        [Column("mes_estatus")]
        public byte Estatus { get; set; }
    }
}