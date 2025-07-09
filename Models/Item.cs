namespace TasTock.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public DateTime CadastradoEm { get; set; } = DateTime.Now;
    }
}
