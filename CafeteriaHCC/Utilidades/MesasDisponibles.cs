namespace CafeteriaHCC.Utilidades
{
    public class MesaDisponible
    {
        public int MesaId { get; set; }
        public int Lugares { get; set; }
    }

    public class MesasDisponiblesResponse
    {
        public int TotalMesasDisponibles { get; set; }
        public List<MesaDisponible> MesasDisponibles { get; set; }
    }
}
