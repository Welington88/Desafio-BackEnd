using BackEnd.Domain.Entities;
using Bogus;
using BackEnd.UnitTests.Common;

namespace BackEnd.UnitTests.Domain.Entity.Motos;

public class MotosTestFixture : BaseFixture
{
    [CollectionDefinition(nameof(MotosTestFixture))]
    public class CategoryTestFixtureCollection
        : ICollectionFixture<MotosTestFixture>
    { }

    public MotosTestFixture() : base() {}

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

    private string? GetValidValidNumberPlate()
    {
        return Faker.Random.Replace("???-#*##");
    }

    private bool GetValidStatus()
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