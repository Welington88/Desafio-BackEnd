using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BackEnd.Domain.SeedWork;
using BackEnd.Domain.Validation;

namespace BackEnd.Domain.Entities;

[Table("Entregadores")]
public class Entregador : Entity
{
    public string? Nome { get; set; }

	public string? CNH { get; set; }

	public string? CategoriaCNH { get; set; }

	public string? CNPJ { get; set; }

	public DateTime DataNascimento { get; set; }

    public string? NumeroCNH { get; set; }

    public bool Ativo { get; set; }

    public Entregador(string? nome, string? cnh, string? categoriaCnh, string? cnpj, DateTime dataNascimento, string numeroCNH, bool ativo)
    {
        ValidadeDomain(nome, cnh, categoriaCnh, cnpj, dataNascimento, numeroCNH, ativo);
    }

    public Entregador()
    {
    }

    [JsonConstructor]
    public Entregador(Guid id, string? nome, string? cnh, string? categoriaCnh, string? cnpj, DateTime dataNascimento, string numeroCNH, bool ativo)
    {
        DomainValidation.When(Guid.Empty == id, "Invalid Id value.");
        Id = id;
        ValidadeDomain(nome, cnh, categoriaCnh, cnpj, dataNascimento, numeroCNH, ativo);
    }

    public void Update(string? nome, string? cnh, string? categoriaCnh, string? cnpj, DateTime dataNascimento, string numeroCNH, bool ativo)
    {
        ValidadeDomain(nome, cnh, categoriaCnh, cnpj, dataNascimento, numeroCNH, ativo);
    }

    private void ValidadeDomain(string? nome, string? cnh, string? categoriaCnh, string? cnpj, DateTime dataNascimento, string numeroCNH, bool? ativo)
    {

        DomainValidation.When(nome!.Length > 50 || string.IsNullOrWhiteSpace(nome), "Ano tem ser maior 1970");
        DomainValidation.When(cnh!.Length > 250 || string.IsNullOrWhiteSpace(cnh), "Ano tem ser maior 1970");
        DomainValidation.When(string.IsNullOrWhiteSpace(cnh) || string.IsNullOrWhiteSpace(numeroCNH), "CNH não pode ser nulo");
        DomainValidation.When(numeroCNH!.Length > 20 || string.IsNullOrWhiteSpace(numeroCNH), "CNH não pode ser maior 20 carecteres");
        DomainValidation.When(string.IsNullOrWhiteSpace(categoriaCnh), "Categoria CNH não pode ser nulo");
        DomainValidation.When(!(categoriaCnh == "A" || categoriaCnh == "B" || categoriaCnh == "AB"), "Categoria CNH Não permitida");
        DomainValidation.When(string.IsNullOrWhiteSpace(cnpj), "CNPJ não pode ser nulo");
        DomainValidation.When(cnpj!.Length > 20, "CNPJ não pode ser maior 20 carecteres");
        DomainValidation.When(dataNascimento == default, "Data de Nascimento Não pode ser nula");
        DomainValidation.When(ativo is null, "Ativo não pode ser Vazio");

        Nome = nome;
        CNH = cnh;
        CategoriaCNH = categoriaCnh;
        NumeroCNH = numeroCNH;
        CNPJ = cnpj;
        DataNascimento = dataNascimento;
        Ativo = (bool)ativo!;
    }

    [NotMapped]
    public Guid? LocacaoId { get; set; }

    [NotMapped]
    public Locacao? Locacao { get; set; }
}