using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.IntegrationTests.Base;

namespace BackEnd.IntegrationTests.Application;

[CollectionDefinition(nameof(PedidosTestFixture))]
public class PedidosRepositoryTestFixtureCollection
    : ICollectionFixture<PedidosTestFixture>
{ }

public class PedidosTestFixture : BaseFixture
{
    public List<Pedido> GetListValidPedidos()
    {
        var ObjectList = new List<Pedido>();
        int numList = new Random().Next(1,100);
        
        for (int objeto = 0; objeto < numList; objeto++)
        {
            ObjectList.Add(GetValidPedido());
        }
        return ObjectList;
    }

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

    public string? GetValidModel()
    {
        return Faker.Vehicle.Model();
    }

    public DateTime GetValidDate()
    {
        return Faker.Date.Past();
    }

    public string GetValidStatus()
    {
        var status = Faker.Random.Enum<StatusPedido>().ToString();
        return status.Replace("í","i");
    }

    public decimal GetValidValidDecimal()
    {
        return Faker.Random.Decimal(1,decimal.MaxValue);
    }

    public decimal GetInvalidDecimal()
    {
        return Faker.Random.Decimal(decimal.MinValue, -1);
    }

    public Guid? GetValidGuid()
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