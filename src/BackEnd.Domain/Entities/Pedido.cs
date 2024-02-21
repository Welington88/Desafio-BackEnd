using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using BackEnd.Domain.Validation;

namespace BackEnd.Domain.Entities;

[Table("Pedidos")]
public class Pedido : Entity
{
    public DateTime DataCriacao { get; set; }

	public string? Status { get; set; }

	public decimal ValorDaCorrida { get; set; }

    public Guid? EntregadorId { get; set; }

    public Pedido()
    {
    }

    public Pedido(DateTime dataCriacao, string? status, decimal valorDaCorrida, Guid? entregadorId)
    {
        ValidadeDomain(dataCriacao, status, valorDaCorrida, entregadorId);
    }

    [JsonConstructor]
    public Pedido(Guid id, DateTime dataCriacao, string? status, decimal valorDaCorrida, Guid? entregadorId)
    {
        DomainValidation.When(Guid.Empty == id, "Invalid Id value.");
        Id = id;
        ValidadeDomain(dataCriacao, status, valorDaCorrida, entregadorId);
    }

    public void Update(DateTime dataCriacao, string? status, decimal valorDaCorrida, Guid? entregadorId)
    {
        ValidadeDomain(dataCriacao, status, valorDaCorrida, entregadorId);
    }

    private void ValidadeDomain(DateTime dataCriacao, string? status, decimal valorDaCorrida, Guid? entregadorId)
    {
        DomainValidation.When(dataCriacao == default, "Data de Criacao Não pode ser nula");
        DomainValidation.When(valorDaCorrida <= 0, "Valor da Corrida Não Pode ser menor ou igual zero");
        DomainValidation.When(string.IsNullOrWhiteSpace(status) , "Status do pedido Não pode ser Nulo");
        DomainValidation.When(!(status == StatusPedido.Entregue.ToString() || status == "Disponível"
            || status == StatusPedido.Aceito.ToString() || status == StatusPedido.Disponivel.ToString()) , 
            "Status do pedido Não Permitido"
        );


        DataCriacao = dataCriacao;
        Status = status;
        ValorDaCorrida = valorDaCorrida;
        EntregadorId = entregadorId;
    }

    public ICollection<Entregador>? Entregadores { get; set; }
}