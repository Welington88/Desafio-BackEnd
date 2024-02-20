using System.ComponentModel.DataAnnotations.Schema;
using BackEnd.Domain.SeedWork;

namespace BackEnd.Domain.Entities;

[Table("Notificacoes")]
public class Notificacao : Entity
{
	public Guid EntregadorId { get; set; }

	public Guid PedidoId { get; set; }

	public DateTime DataNoticacao { get; set; }

	public ICollection<Entregador>? Entregadores { get; set; }

    public ICollection<Pedido>? Pedidos { get; set; }
}