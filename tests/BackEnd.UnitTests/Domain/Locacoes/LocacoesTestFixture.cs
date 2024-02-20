using Bogus;
using BackEnd.UnitTests.Common;
using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;

namespace BackEnd.UnitTests.Domain.Entity.Locacoes;

public class LocacoesTestFixture : BaseFixture
{
    [CollectionDefinition(nameof(LocacoesTestFixture))]
    public class CategoryTestFixtureCollection
        : ICollectionFixture<LocacoesTestFixture>
    { }

    public LocacoesTestFixture() : base() {}

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

    private DateTime GetValidDateCreated()
    {
        return Faker.Date.Future();
    }

    private int GalidDaysForPlan()
    {
        var listValues = new List<int>() { 7, 15, 30 };
        return (int)Faker.Random.ListItem(listValues);
    }

    private decimal GetValidValuePlan(string validDaysForPlan)
    {
        var planValueDays = Enum.Parse<Planos>(validDaysForPlan);

        return (int)planValueDays;
    }

    private string? GetValidStatus()
    {
        return Faker.Random.Enum<StatusLocacao>().ToString();
    }

    public Locacao GetValidLocacao()
    {
        var ObjectValid = new Locacao();

        var validDateCreated = GetValidDateCreated();
        int validDaysForPlan = GalidDaysForPlan();
        ObjectValid.Plano = $"_{validDaysForPlan}dias";
        ObjectValid.PrazoEmDias = validDaysForPlan;
        ObjectValid.DataCriacao = validDateCreated;
        ObjectValid.DataInicio = validDateCreated.AddDays(1);
        ObjectValid.DataTermino = validDateCreated.AddDays(validDaysForPlan);
        ObjectValid.DataPrevistaTermino = validDateCreated.AddDays(validDaysForPlan);
        ObjectValid.ValorDiaria = GetValidValuePlan(ObjectValid.Plano);
        ObjectValid.ValorTotal = ObjectValid.ValorDiaria + validDaysForPlan;
        ObjectValid.EntregadorId = Guid.NewGuid();
        ObjectValid.MotoId  = Guid.NewGuid();
        ObjectValid.Status = GetValidStatus();

        return ObjectValid;
    }

    
}