using FluentAssertions;
using BackEnd.UnitTests.Common;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Validation;

namespace BackEnd.UnitTests.Domain.Entity.Pedidos;

[Collection(nameof(PedidosTestFixture))]
public class PedidosTest : BaseFixture
{
    private readonly PedidosTestFixture _fixture;

    public PedidosTest(PedidosTestFixture fixture) 
        => _fixture = fixture;

    private static void InstancePedido(Pedido validPedido, ref Pedido Pedido)
    {
        Pedido.DataCriacao = validPedido.DataCriacao;
        Pedido.ValorDaCorrida = validPedido.ValorDaCorrida;
        Pedido.EntregadorId = validPedido.EntregadorId;
        Pedido.Status = validPedido.Status;
    }

    [Fact(DisplayName = nameof(InstantiatePedido))]
    [Trait("Domain", "Pedido - Entity")]
    public void InstantiatePedido()
    {
        var validPedido = _fixture.GetValidPedido();

        var Pedido = new Pedido();
        InstancePedido(validPedido, ref Pedido);

        Pedido.Should().NotBeNull();
        Pedido.DataCriacao.Should().Be(validPedido.DataCriacao);
        Pedido.DataCriacao.Should().NotBe(default(DateTime));
        (Pedido.ValorDaCorrida < 0).Should().BeFalse();
        Pedido.ValorDaCorrida.Should().Be(validPedido.ValorDaCorrida);
        Pedido.EntregadorId.Should().Be(validPedido.EntregadorId);
        Pedido.EntregadorId.Should().NotBe(default(Guid));
        (Pedido.Status!.Length <= 10).Should().BeTrue();
        Pedido.Status.Should().Be(validPedido.Status);
    }

    [Fact(DisplayName = nameof(InstantiateErrorPedidoDataCriacaoIsNull))]
    [Trait("Domain", "Pedido - Entity")]
    public void InstantiateErrorPedidoDataCriacaoIsNull()
    {
        var NewInvalidDate = default(DateTime);
        var validPedido = _fixture.GetValidPedido();
        Action action = () => new Pedido(NewInvalidDate, validPedido.Status, validPedido.ValorDaCorrida, validPedido.EntregadorId);
         
        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Data de Criacao Não pode ser nula");
    }

    [Fact(DisplayName = nameof(InstantiateErrorPedidoValorCorridaLessZero))]
    [Trait("Domain", "Pedido - Entity")]
    public void InstantiateErrorPedidoValorCorridaLessZero()
    {
        var invalidValue = _fixture.GetInvalidDecimal();
        var validPedido = _fixture.GetValidPedido();
        Action action = () => new Pedido(validPedido.DataCriacao, validPedido.Status, invalidValue, validPedido.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Valor da Corrida Não Pode ser menor ou igual zero");
    }

    [Theory(DisplayName = nameof(InstantiateErrorPedidoValorCorridaIsNull))]
    [Trait("Domain", "Pedido - Entity")]
    [InlineData("    ")]
    [InlineData("")]
    [InlineData(null)]
    public void InstantiateErrorPedidoValorCorridaIsNull(string invalidValue)
    {
        var validPedido = _fixture.GetValidPedido();
        Action action = () => new Pedido(validPedido.DataCriacao, invalidValue, validPedido.ValorDaCorrida, validPedido.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Status do pedido Não pode ser Nulo");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenMacAdressIsNullorEmpty))]
    [Trait("Domain", "Pedido - Entity")]
    public void InstantiateErrorWhenMacAdressIsNullorEmpty()
    {
        var invalidValue = _fixture.GetValidInvalidString(11);

        var validPedido = _fixture.GetValidPedido();
        Action action = () => new Pedido(validPedido.DataCriacao, invalidValue, validPedido.ValorDaCorrida, validPedido.EntregadorId);

        action.Should()
            .Throw<DomainValidation>()
            .WithMessage("Status do pedido Não Permitido");
    }
}