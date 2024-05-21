using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CafeteriaHCC.Models
{
    [Table("Tb_HccOrdenes")]
    public class Ordenes
    {
        [Key]
        [Column("ord_id")]
        public int Id { get; set; }

        [Column("mes_id")]
        public int MesaId { get; set; }

        [ForeignKey("MesaId")]
        public Mesa Mesa { get; set; }

        [Column("catord_id")]
        public int CategoriaOrdenId { get; set; }

        [ForeignKey("CategoriaOrdenId")]
        public CatEstatusOrden CategoriaOrden { get; set; }

        [Column("ord_fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [Column("ord_estatus")]
        public byte Estatus { get; set; }
    }
}