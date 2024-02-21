using BackEnd.Domain.Entities;
using BackEnd.Domain.Enum;
using BackEnd.IntegrationTests.Base;
using Bogus.Extensions.Brazil;

namespace BackEnd.IntegrationTests.Application;

[CollectionDefinition(nameof(EntregadoresTestFixture))]
public class EntregadoresRepositoryTestFixtureCollection
    : ICollectionFixture<EntregadoresTestFixture>
{ }

public class EntregadoresTestFixture : BaseFixture
{
    public List<Entregador> GetListValidEntregadores()
    {
        var ObjectList = new List<Entregador>();
        int numList = new Random().Next(1,100);
        
        for (int objeto = 0; objeto < numList; objeto++)
        {
            ObjectList.Add(GetValidEntregador());
        }
        return ObjectList;
    }

    public string GetValidInvalidString(int max)
    {
        return Faker.Random.String2(max);
    }

    public bool GetValidStatus()
    {
        return Faker.Random.Bool();
    }

    public string? GetValidValidImagemCNH()
    {
        return Faker.Image.PlaceImgUrl();
    }

    public DateTime GetValidValidDataNascimento()
    {
        return Faker.Person.DateOfBirth;
    }

    public string? GetValidValidNome()
    {
        var name = Faker.Person.FullName;

        if (name.Length > 50)
            name = name.Substring(0, 49);

        return name;
    }

    public string? GetValidValidCategoriaCNH()
    {
        return Faker.Random.Enum<Categoria_CNH>().ToString();
    }

    public string? GetValidNumeroCNH()
    {
        return Faker.Random.Replace("####################");
    }

    public string? GetValidCNPJ()
    {
        return Faker.Company.Cnpj();
    }

    public Entregador GetValidEntregador()
    {
        var ObjectValid = new Entregador();

        ObjectValid.CNPJ = GetValidCNPJ();
        ObjectValid.NumeroCNH = GetValidNumeroCNH();
        ObjectValid.CategoriaCNH = GetValidValidCategoriaCNH();
        ObjectValid.Nome = GetValidValidNome();
        ObjectValid.DataNascimento = GetValidValidDataNascimento();
        ObjectValid.CNH = GetValidValidImagemCNH();
        ObjectValid.Ativo = GetValidStatus();

        return ObjectValid;
    }
}