using System.Net.NetworkInformation;

namespace AplicacaoWeb.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Titulo { get; set;}
        public string Descricao { get; set; } 
        public int Pontuacao { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public Status Status { get; set; }

    }
}
