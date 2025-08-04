namespace TasTock.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string Tipo { get; set; } = string.Empty; // "Venda" ou "Reposição"
        public string NomeItem { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorAntes { get; set; }
        public decimal ValorDepois { get; set; }

    }
}
