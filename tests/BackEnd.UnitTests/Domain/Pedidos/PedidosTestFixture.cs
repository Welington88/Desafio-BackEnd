using Bogus;
using BackEnd.UnitTests.Common;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;

namespace BackEnd.UnitTests.Domain.Entity.Pedidos;

public class PedidosTestFixture : BaseFixture
{
    [CollectionDefinition(nameof(PedidosTestFixture))]
    public class CategoryTestFixtureCollection
        : ICollectionFixture<PedidosTestFixture>
    { }

    public PedidosTestFixture() : base() {}

    public string GetValidInvalidString(int max)
    {
        return Faker.Random.String2(max);
    }

    public int GetValidYear()
    {
        var ValidId = int.MinValue;
        do
        {
            ValidId = Faker.Date.Past(20).Year;
        }while (ValidId < 0);

        return ValidId;
    }

    private string? GetValidModel()
    {
        return Faker.Vehicle.Model();
    }

    private DateTime GetValidDate()
    {
        return Faker.Date.Past();
    }

    private string GetValidStatus()
    {
        return Faker.Random.Enum<StatusPedido>().ToString();
    }

    private decimal GetValidValidDecimal()
    {
        return Faker.Random.Decimal(1,decimal.MaxValue);
    }

    internal decimal GetInvalidDecimal()
    {
        return Faker.Random.Decimal(decimal.MinValue, -1);
    }

    private Guid? GetValidGuid()
    {
        return Faker.Random.Guid();
    }

    public Pedido GetValidPedido()
    {
        var ObjectValid = new Pedido();

        ObjectValid.DataCriacao = GetValidDate();
        ObjectValid.Status = GetValidStatus();
        ObjectValid.ValorDaCorrida = GetValidValidDecimal();
        ObjectValid.EntregadorId = GetValidGuid();
            
        return ObjectValid;
    }

    
}