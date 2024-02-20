using BackEnd.Domain.Entities;
using BackEnd.Domain.Validation;
using FluentAssertions;
using BackEnd.UnitTests.Common;

namespace BackEnd.UnitTests.Domain.Entity.Motos;

[Collection(nameof(MotosTestFixture))]
public class MotosTest : BaseFixture
{
    private readonly MotosTestFixture _fixture;

    public MotosTest(MotosTestFixture fixture) 
        => _fixture = fixture;

    private static void InstanceMoto(Moto validMoto, ref Moto Moto)
    {
        Moto.Ano = validMoto.Ano;
        Moto.Modelo = validMoto.Modelo;
        Moto.Placa = validMoto.Placa;
        Moto.Ativo = validMoto.Ativo;
    }

    [Fact(DisplayName = nameof(InstantiateMoto))]
    [Trait("Domain", "Moto - Entity")]
    public void InstantiateMoto()
    {
        var validMoto = _fixture.GetValidMoto();

        var Moto = new Moto();
        InstanceMoto(validMoto, ref Moto);

        Moto.Should().NotBeNull();
        Moto.Ano.Should().Be(validMoto.Ano);
        Moto.Ano.Should().NotBe(default(int));
        (Moto.Ano < 0).Should().BeFalse();
        Moto.Modelo.Should().Be(validMoto.Modelo);
        Moto.Modelo.Should().NotBeNullOrWhiteSpace();
        (Moto.Modelo!.Length <= 50).Should().BeTrue();
        Moto.Placa.Should().Be(validMoto.Placa);
        Moto.Placa.Should().NotBeNullOrWhiteSpace();
        (Moto.Placa!.Length <= 10).Should().BeTrue();
        Moto.Ativo.Should().Be(validMoto.Ativo);
    }

    [Fact(DisplayName = nameof(InstantiateErrorMotoYear))]
    [Trait("Domain", "Moto - Entity")]
    public void InstantiateErrorMotoYear()
    {
        var NewYearInvalid = new Random().Next(int.MinValue, 1969);

        var validMoto = _fixture.GetValidMoto();

        Action action = () => new Moto(NewYearInvalid, validMoto.Modelo, validMoto.Placa, validMoto.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Ano tem ser maior 1970");
    }

    [Theory(DisplayName = nameof(InstantiateErrorMotoModel))]
    [Trait("Domain", "Moto - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null!)]
    public void InstantiateErrorMotoModel(string invalidValue)
    {
        var validMoto = _fixture.GetValidMoto();

        Action action = () => new Moto(validMoto.Ano, invalidValue, validMoto.Placa, validMoto.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Modelo não pode ser nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorMotoModelLarger50))]
    [Trait("Domain", "Moto - Entity")]
    public void InstantiateErrorMotoModelLarger50()
    {
        var invalidValue = _fixture.GetValidInvalidString(51);

        var validMoto = _fixture.GetValidMoto();

        Action action = () => new Moto(validMoto.Ano, invalidValue, validMoto.Placa, validMoto.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Modelo não pode ser maior que 50 carecteres");
    }

    [Theory(DisplayName = nameof(InstantiateErrorMotoPlaca))]
    [Trait("Domain", "Moto - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void InstantiateErrorMotoPlaca(string invalidValue)
    {
        var validMoto = _fixture.GetValidMoto();

        Action action = () => new Moto(validMoto.Ano, validMoto.Modelo, invalidValue, validMoto.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Placa não pode ser nula");
    }

    [Fact(DisplayName = nameof(InstantiateErrorMotoPlacaLarger10))]
    [Trait("Domain", "Moto - Entity")]
    public void InstantiateErrorMotoPlacaLarger10()
    {
        var invalidValue = _fixture.GetValidInvalidString(11);

        var validMoto = _fixture.GetValidMoto();

        Action action = () => new Moto(validMoto.Ano, validMoto.Modelo, invalidValue, validMoto.Ativo);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Placa não pode ser maior que 10 carecteres");
    }
}