using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.IntegrationTests.Base;

namespace BackEnd.IntegrationTests.Application;

[CollectionDefinition(nameof(LocacoesTestFixture))]
public class LocacoesRepositoryTestFixtureCollection
    : ICollectionFixture<LocacoesTestFixture>
{ }

public class LocacoesTestFixture : BaseFixture
{
    public List<Locacao> GetListValidLocacoes()
    {
        var ObjectList = new List<Locacao>();
        int numList = new Random().Next(1,100);
        
        for (int objeto = 0; objeto < numList; objeto++)
        {
            ObjectList.Add(GetValidLocacao());
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

    public DateTime GetValidDateCreated()
    {
        return Faker.Date.Future();
    }

    public int GalidDaysForPlan()
    {
        var listValues = new List<int>() { 7, 15, 30 };
        return (int)Faker.Random.ListItem(listValues);
    }

    public decimal GetValidValuePlan(string validDaysForPlan)
    {
        var planValueDays = Enum.Parse<Planos>(validDaysForPlan);

        return (int)planValueDays;
    }

    public string? GetValidStatus()
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