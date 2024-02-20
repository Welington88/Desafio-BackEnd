using FluentAssertions;
using BackEnd.UnitTests.Common;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Validation;

namespace BackEnd.UnitTests.Domain.Entity.Locacoes;

[Collection(nameof(LocacoesTestFixture))]
public class LocacoesTest : BaseFixture
{
    private readonly LocacoesTestFixture _fixture;

    public LocacoesTest(LocacoesTestFixture fixture) 
        => _fixture = fixture;

    private static void InstanceLocacao(Locacao validLocacao, ref Locacao Locacao)
    {
        Locacao.Plano = validLocacao.Plano;
        Locacao.PrazoEmDias = validLocacao.PrazoEmDias;
        Locacao.DataCriacao = validLocacao.DataCriacao;
        Locacao.DataInicio = validLocacao.DataInicio;
        Locacao.DataTermino = validLocacao.DataTermino;
        Locacao.DataPrevistaTermino = validLocacao.DataPrevistaTermino;
        Locacao.ValorDiaria = validLocacao.ValorDiaria;
        Locacao.ValorTotal = validLocacao.ValorTotal;
        Locacao.EntregadorId = validLocacao.EntregadorId;
        Locacao.MotoId = validLocacao.MotoId;
        Locacao.Status = validLocacao.Status;
    }

    [Fact(DisplayName = nameof(InstantiateLocacao))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateLocacao()
    {
        var validLocacao = _fixture.GetValidLocacao();

        var Locacao = new Locacao();
        InstanceLocacao(validLocacao, ref Locacao);

        Locacao.Should().NotBeNull();
        Locacao.PrazoEmDias.Should().Be(validLocacao.PrazoEmDias);
        Locacao.PrazoEmDias.Should().NotBe(default(int));
        (Locacao.PrazoEmDias < 0).Should().BeFalse();
        Locacao.Plano.Should().Be(validLocacao.Plano);
        Locacao.Plano.Should().NotBeNullOrWhiteSpace();
        (Locacao.Plano!.Length <= 20).Should().BeTrue();
        Locacao.Status.Should().Be(validLocacao.Status);
        Locacao.Status.Should().NotBeNullOrWhiteSpace();
        (Locacao.Status!.Length <= 10).Should().BeTrue();
        Locacao.PrazoEmDias.Should().Be(validLocacao.PrazoEmDias);
        Locacao.PrazoEmDias.Should().NotBe(default(int));

        Locacao.ValorDiaria.Should().Be(validLocacao.ValorDiaria);
        Locacao.ValorDiaria.Should().NotBe(default(int));
        (Locacao.ValorDiaria < 0).Should().BeFalse();

        Locacao.ValorTotal.Should().Be(validLocacao.ValorTotal);
        Locacao.ValorTotal.Should().NotBe(default(int));
        (Locacao.ValorTotal < 0).Should().BeFalse();

        Locacao.EntregadorId.Should().NotBe(default(Guid));
        Locacao.MotoId.Should().NotBe(default(Guid));
    }

    [Fact(DisplayName = nameof(InstantiateErrorInvalidPrazoEmDiasLessZero))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateErrorInvalidPrazoEmDiasLessZero()
    {
        var NewInvalidValue = new Random().Next(31, int.MaxValue);

        var validLocacao = _fixture.GetValidLocacao();
        
        Action action = () => new Locacao(validLocacao.Plano, NewInvalidValue, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, validLocacao.Status, validLocacao.MotoId, validLocacao.EntregadorId);
         
        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Prazo em dias Invalido");
    }

    [Theory(DisplayName = nameof(InstantiateErrorInvalidPlanoIsNull))]
    [Trait("Domain", "Locacao - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void InstantiateErrorInvalidPlanoIsNull(string invalidValue)
    {
        var validLocacao = _fixture.GetValidLocacao();
        
        Action action = () => new Locacao(invalidValue, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, validLocacao.Status, validLocacao.MotoId, validLocacao.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Plano não pode ser nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorInvalidStatusLarge20))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateErrorInvalidStatusLarge20()
    {
        var invalidValue =_fixture.GetValidInvalidString(21);

        var validLocacao = _fixture.GetValidLocacao();
        
        Action action = () => new Locacao(invalidValue, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, validLocacao.Status, validLocacao.MotoId, validLocacao.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Plano não pode ser maior que 20 caracteres");
    }

    [Theory(DisplayName = nameof(InstantiateErrorInvalidStatusIsNull))]
    [Trait("Domain", "Locacao - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void InstantiateErrorInvalidStatusIsNull(string invalidValue)
    {
        var validLocacao = _fixture.GetValidLocacao();
        
        Action action = () => new Locacao(validLocacao.Plano, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, invalidValue, validLocacao.MotoId, validLocacao.EntregadorId);
        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Status não pode ser nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorInvalidStatusLarger10))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateErrorInvalidStatusLarger10()
    {
        var invalidValue = _fixture.GetValidInvalidString(11);

        var validLocacao = _fixture.GetValidLocacao();
        
        Action action = () => new Locacao(validLocacao.Plano, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, invalidValue, validLocacao.MotoId, validLocacao.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Status não pode ser maior que 10 caracteres ou não é Ativa e Finalizada");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenValueDayLessZero))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateErrorWhenValueDayLessZero()
    {
        var invalidValue = new Random().Next(int.MinValue, 0);

        var validLocacao = _fixture.GetValidLocacao();

        Action action = () => new Locacao(validLocacao.Plano, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, invalidValue, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, validLocacao.Status , validLocacao.MotoId, validLocacao.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Valor Diaria não pode ser menor ou igual zero");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenValueFinalLessZero))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateErrorWhenValueFinalLessZero()
    {
        var invalidValue = new Random().Next(int.MinValue, 0);

        var validLocacao = _fixture.GetValidLocacao();

        Action action = () => new Locacao(validLocacao.Plano, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, invalidValue, validLocacao.Status , validLocacao.MotoId, validLocacao.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Valor Total não pode ser menor ou igual zero");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenEntregadorIsNull))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateErrorWhenEntregadorIsNull()
    {
        var invalidValue = default(Guid);

        var validLocacao = _fixture.GetValidLocacao();

        Action action = () => new Locacao(validLocacao.Plano, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, validLocacao.Status , validLocacao.MotoId, invalidValue);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("EntregadorId não pode ser nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenMotoIsNull))]
    [Trait("Domain", "Locacao - Entity")]
    public void InstantiateErrorWhenMotoIsNull()
    {
        var invalidValue = default(Guid);

        var validLocacao = _fixture.GetValidLocacao();

        Action action = () => new Locacao(validLocacao.Plano, validLocacao.PrazoEmDias, validLocacao.DataCriacao, validLocacao.DataInicio, validLocacao.DataTermino, validLocacao.DataPrevistaTermino, validLocacao.ValorDiaria, validLocacao.ValorAdicional, validLocacao.ValorMulta, validLocacao.ValorTotal, validLocacao.Status , invalidValue, validLocacao.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("MotoId não pode ser nulo");
    }
}