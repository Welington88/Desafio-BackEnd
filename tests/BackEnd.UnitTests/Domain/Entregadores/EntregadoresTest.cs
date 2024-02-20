using FluentAssertions;
using BackEnd.UnitTests.Common;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Validation;

namespace BackEnd.UnitTests.Domain.Entity.Entregadores;

[Collection(nameof(EntregadoresTestFixture))]
public class EntregadoresTest : BaseFixture
{
    private readonly EntregadoresTestFixture _fixture;

    public EntregadoresTest(EntregadoresTestFixture fixture) 
        => _fixture = fixture;

    private static void InstanceEntregador(Entregador validEntregador, ref Entregador Entregador)
    {
        Entregador.CNPJ = validEntregador.CNPJ;
        Entregador.NumeroCNH = validEntregador.NumeroCNH;
        Entregador.CategoriaCNH = validEntregador.CategoriaCNH;
        Entregador.Nome = validEntregador.Nome;
        Entregador.DataNascimento = validEntregador.DataNascimento;
        Entregador.CNH = validEntregador.CNH;
        Entregador.Ativo = validEntregador.Ativo;
    }

    [Fact(DisplayName = nameof(InstantiateEntregador))]
    [Trait("Domain", "Entregador - Entity")]
    public void InstantiateEntregador()
    {
        var validEntregador = _fixture.GetValidEntregador();

        var Entregador = new Entregador();
        InstanceEntregador(validEntregador, ref Entregador);

        Entregador.Should().NotBeNull();
        Entregador.CNPJ.Should().Be(validEntregador.CNPJ);
        Entregador.CNPJ.Should().NotBeNull();
        (Entregador.CNPJ!.Length <= 20).Should().BeTrue();
        Entregador.NumeroCNH.Should().Be(validEntregador.NumeroCNH);
        Entregador.NumeroCNH.Should().NotBeNull();
        (Entregador.NumeroCNH!.Length <= 20).Should().BeTrue();
        Entregador.CategoriaCNH.Should().Be(validEntregador.CategoriaCNH);
        Entregador.CategoriaCNH.Should().NotBeNull();
        (Entregador.CategoriaCNH!.Length <= 2).Should().BeTrue();
        Entregador.DataNascimento.Should().Be(validEntregador.DataNascimento);
        Entregador.DataNascimento.Should().NotBe(default(DateTime));
        Entregador.Ativo.Should().Be(validEntregador.Ativo);
    }

    [Fact(DisplayName = nameof(InstantiateErrorEntregadorCNPJlLarger20))]
    [Trait("Domain", "Entregador - Entity")]
    public void InstantiateErrorEntregadorCNPJlLarger20()
    {
        var validEntregador = _fixture.GetValidEntregador();
        var invalidValue = _fixture.GetValidInvalidString(21);

        Action action = () => new Entregador(validEntregador.Nome, validEntregador.CNH, validEntregador.CategoriaCNH, invalidValue, validEntregador.DataNascimento, validEntregador.NumeroCNH!, validEntregador.Ativo);
         
        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("CNPJ não pode ser maior 20 carecteres");
    }

    [Theory(DisplayName = nameof(InstantiateErrorEntregadorCNPJIsNull))]
    [Trait("Domain", "Entregador - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void InstantiateErrorEntregadorCNPJIsNull(string invalidValue)
    {
        var validEntregador = _fixture.GetValidEntregador();

        Action action = () => new Entregador(validEntregador.Nome, validEntregador.CNH, validEntregador.CategoriaCNH, invalidValue, validEntregador.DataNascimento, validEntregador.NumeroCNH!, validEntregador.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("CNPJ não pode ser nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorEntregadorCnhLarger20))]
    [Trait("Domain", "Entregador - Entity")]
    public void InstantiateErrorEntregadorCnhLarger20()
    {
        var invalidValue =  _fixture.GetValidInvalidString(21);
        var validEntregador = _fixture.GetValidEntregador();

        Action action = () => new Entregador(validEntregador.Nome, validEntregador.CNH, validEntregador.CategoriaCNH, validEntregador.CNPJ , validEntregador.DataNascimento, invalidValue, validEntregador.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("CNH não pode ser maior 20 carecteres");
    }

    [Theory(DisplayName = nameof(InstantiateErrorEntregadorCnhIsNull))]
    [Trait("Domain", "Entregador - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void InstantiateErrorEntregadorCnhIsNull(string invalidValue)
    {
        var validEntregador = _fixture.GetValidEntregador();

        Action action = () => new Entregador(validEntregador.Nome, validEntregador.CNH, validEntregador.CategoriaCNH, validEntregador.CNPJ, validEntregador.DataNascimento, invalidValue, validEntregador.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("CNH não pode ser nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorEntregadorCategoriaInvalidCnh))]
    [Trait("Domain", "Entregador - Entity")]
    public void InstantiateErrorEntregadorCategoriaInvalidCnh()
    {
        var Entregador = new Entregador();
        var invalidValue = _fixture.GetValidInvalidString(3).Replace("A","X");
        var validEntregador = _fixture.GetValidEntregador();
        Action action = () => new Entregador(validEntregador.Nome, validEntregador.CNH, invalidValue, validEntregador.CNPJ, validEntregador.DataNascimento, validEntregador.NumeroCNH!, validEntregador.Ativo);
        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Categoria CNH Não permitida");
    }

    [Theory(DisplayName = nameof(InstantiateErrorEntregadorCategoriaCnhIsNull))]
    [Trait("Domain", "Entregador - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void InstantiateErrorEntregadorCategoriaCnhIsNull(string invalidValue)
    {
        var validEntregador = _fixture.GetValidEntregador();
        Action action = () => new Entregador(validEntregador.Nome, validEntregador.CNH, invalidValue, validEntregador.CNPJ, validEntregador.DataNascimento, validEntregador.NumeroCNH!, validEntregador.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Categoria CNH não pode ser nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorEntregadorDataNascimentoIsNull))]
    [Trait("Domain", "Entregador - Entity")]
    public void InstantiateErrorEntregadorDataNascimentoIsNull()
    {
        var invalidValue = default(DateTime);

        var validEntregador = _fixture.GetValidEntregador();
        Action action = () => new Entregador(validEntregador.Nome, validEntregador.CNH, validEntregador.CategoriaCNH, validEntregador.CNPJ, invalidValue, validEntregador.NumeroCNH!, validEntregador.Ativo);
               
        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Data de Nascimento Não pode ser nula");
    }
}