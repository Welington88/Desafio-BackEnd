using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BackEnd.Domain.SeedWork;
using BackEnd.Domain.Validation;

namespace BackEnd.Domain.Entities;

public sealed class Moto : Entity
{
    public int Ano { get; set; }

	public string? Modelo { get; set; }

	public string? Placa { get; set; }

	public bool Ativo { get; set; }

    public Moto()
    {
    }

    public Moto(int ano, string? modelo, string? placa, bool ativo)
    {
        ValidadeDomain(ano, modelo, placa, ativo);
    }

    [JsonConstructor]
    public Moto(Guid id, int ano, string? modelo, string? placa, bool ativo)
    {
        DomainValidation.When(Guid.Empty==id, "Invalid Id value.");
        Id = id;
        ValidadeDomain(ano, modelo, placa, ativo);
    }

    public void Update(int ano, string? modelo, string? placa, bool ativo)
    {
        ValidadeDomain(ano, modelo, placa, ativo);
    }

    private void ValidadeDomain(int ano, string? modelo, string? placa, bool? ativo)
    {


        DomainValidation.When(ano < 1970, "Ano tem ser maior 1970");
        DomainValidation.When(string.IsNullOrWhiteSpace(modelo), "Modelo não pode ser nulo");
        DomainValidation.When(modelo!.Length > 50, "Modelo não pode ser maior que 50 carecteres");
        DomainValidation.When(string.IsNullOrWhiteSpace(placa), "Placa não pode ser nula");
        DomainValidation.When(placa!.Length > 10, "Placa não pode ser maior que 10 carecteres");
        DomainValidation.When(ativo is null , "Ativo não pode ser Vazio");

        Ano = ano;
        Modelo = modelo;
        Placa = placa;
        Ativo = (bool)ativo!;
    }

    [NotMapped]
    public Guid? LocacaoId { get; set; }

    [NotMapped]
    public Locacao? Locacao { get; set; }
}