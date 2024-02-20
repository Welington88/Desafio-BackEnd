using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BackEnd.Domain.Enum;
using BackEnd.Domain.SeedWork;
using BackEnd.Domain.Validation;

namespace BackEnd.Domain.Entities;

[Table("Locacoes")]
public class Locacao : Entity
{

    public string? Plano { get; set; }

    public int PrazoEmDias { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataTermino { get; set; }

    public DateTime DataPrevistaTermino { get; set; }

    public decimal ValorDiaria { get; set; }

    public decimal? ValorAdicional { get; set; }

    public decimal? ValorMulta { get; set; }

    public decimal ValorTotal { get; set; }

    public string? Status { get; set; }

    public Guid? MotoId { get; set; }

    public Guid? EntregadorId { get; set; }

    public Locacao()
    {
    }

    public Locacao(string? plano, int prazoEmDias, DateTime dataCriacao, DateTime dataInicio, DateTime dataTermino, DateTime dataPrevistaTermino, decimal valorDiaria, decimal? valorAdicional, decimal? valorMulta, decimal valorTotal, string? status, Guid? motoId, Guid? entregadorId)
    {
        ValidadeDomain(plano, prazoEmDias, dataCriacao, dataInicio, dataTermino, dataPrevistaTermino, valorDiaria, valorAdicional, valorMulta, valorTotal, status, motoId, entregadorId);
    }

    [JsonConstructor]
    public Locacao(Guid id, string? plano, int prazoEmDias, DateTime dataCriacao, DateTime dataInicio, DateTime dataTermino, DateTime dataPrevistaTermino, decimal valorDiaria, decimal? valorAdicional, decimal? valorMulta, decimal valorTotal, string? status, Guid? motoId, Guid? entregadorId)
    {
        DomainValidation.When(Guid.Empty == id, "Invalid Id value.");
        Id = id;
        ValidadeDomain(plano, prazoEmDias, dataCriacao, dataInicio, dataTermino, dataPrevistaTermino, valorDiaria, valorAdicional, valorMulta, valorTotal, status, motoId, entregadorId);
    }

    public void Update(string? plano, int prazoEmDias, DateTime dataCriacao, DateTime dataInicio, DateTime dataTermino, DateTime dataPrevistaTermino, decimal valorDiaria, decimal? valorAdicional, decimal? valorMulta, decimal valorTotal, string? status, Guid? motoId, Guid? entregadorId)
    {
        ValidadeDomain(plano, prazoEmDias, dataCriacao, dataInicio, dataTermino, dataPrevistaTermino, valorDiaria, valorAdicional, valorMulta, valorTotal, status, motoId, entregadorId);
    }

    private void ValidadeDomain(string? plano, int prazoEmDias, DateTime dataCriacao, DateTime dataInicio, DateTime dataTermino, DateTime dataPrevistaTermino, decimal valorDiaria, decimal? valorAdicional, decimal? valorMulta, decimal valorTotal, string? status, Guid? motoId, Guid? entregadorId)
    {
        DomainValidation.When(!(prazoEmDias==7 || prazoEmDias==15 || prazoEmDias == 30) , "Prazo em dias Invalido");
        DomainValidation.When(string.IsNullOrWhiteSpace(plano), "Plano não pode ser nulo");
        DomainValidation.When(plano!.Length > 20, "Plano não pode ser maior que 20 caracteres");
        DomainValidation.When(valorDiaria <=0 , "Valor Diaria não pode ser menor ou igual zero");
        DomainValidation.When(valorTotal <=0 , "Valor Total não pode ser menor ou igual zero");
        DomainValidation.When(string.IsNullOrWhiteSpace(status), "Status não pode ser nulo");
        DomainValidation.When(entregadorId == default(Guid), "EntregadorId não pode ser nulo");
        DomainValidation.When(motoId == default(Guid), "MotoId não pode ser nulo");
        DomainValidation.When(!(status!.Length <= 10 ||
           status == StatusLocacao.Finalizada.ToString()
           || status == StatusLocacao.Ativa.ToString()) , "Status não pode ser maior que 10 caracteres ou não é Ativa e Finalizada");
        
        Plano = plano;
        PrazoEmDias = prazoEmDias;
        DataCriacao = dataCriacao;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
        DataPrevistaTermino = dataPrevistaTermino;
        ValorDiaria = valorDiaria;
        ValorAdicional = valorAdicional;
        ValorMulta = valorMulta;
        ValorTotal = valorTotal;
        Status = status;
        MotoId = motoId;
        EntregadorId = entregadorId;
    }

    public ICollection<Moto>? Motos { get; set; }

    public ICollection<Entregador>? Entregadores { get; set; }
}