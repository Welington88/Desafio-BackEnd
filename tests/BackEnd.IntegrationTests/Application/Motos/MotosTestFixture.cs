using BackEnd.Domain.Entities;
using BackEnd.IntegrationTests.Base;

namespace BackEnd.IntegrationTests.Application;

[CollectionDefinition(nameof(MotosTestFixture))]
public class MotosRepositoryTestFixtureCollection
    : ICollectionFixture<MotosTestFixture>
{ }

public class MotosTestFixture : BaseFixture
{
    public List<Moto> GetListValidMotos()
    {
        var ObjectList = new List<Moto>();
        int numList = new Random().Next(1,100);
        
        for (int objeto = 0; objeto < numList; objeto++)
        {
            ObjectList.Add(GetValidMoto());
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
        } while (ValidId < 0);

        return ValidId;
    }

    public string? GetValidModel()
    {
        return Faker.Vehicle.Model();
    }

    public string? GetValidValidNumberPlate()
    {
        return Faker.Random.Replace("???-#*##");
    }

    public bool GetValidStatus()
    {
        return Faker.Random.Bool();
    }

    public Moto GetValidMoto()
    {
        var ObjectValid = new Moto();

        ObjectValid.Ano = GetValidYear();
        ObjectValid.Modelo = GetValidModel();
        ObjectValid.Placa = GetValidValidNumberPlate();
        ObjectValid.Ativo = GetValidStatus();

        return ObjectValid;
    }
}