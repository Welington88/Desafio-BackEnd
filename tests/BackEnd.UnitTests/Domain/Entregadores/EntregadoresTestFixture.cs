using Bogus;
using BackEnd.UnitTests.Common;
using BackEnd.Domain.Entities;
using Bogus.Extensions.Brazil;
using BackEnd.Domain.Enum;

namespace BackEnd.UnitTests.Domain.Entity.Entregadores;

public class EntregadoresTestFixture : BaseFixture
{
    [CollectionDefinition(nameof(EntregadoresTestFixture))]
    public class CategoryTestFixtureCollection
        : ICollectionFixture<EntregadoresTestFixture>
    { }

    public EntregadoresTestFixture() : base() {}

    public string GetValidInvalidString(int max)
    {
        return Faker.Random.String2(max);
    }

    private bool GetValidStatus()
    {
        return Faker.Random.Bool();
    }

    private string? GetValidValidImagemCNH()
    {
        return Faker.Image.PlaceImgUrl();
    }

    private DateTime GetValidValidDataNascimento()
    {
        return Faker.Person.DateOfBirth;
    }

    private string? GetValidValidNome()
    {
        var name = Faker.Person.FullName;

        if (name.Length > 50)
            name = name.Substring(0, 49);

        return name;
    }

    private string? GetValidValidCategoriaCNH()
    {
        return Faker.Random.Enum<Categoria_CNH>().ToString();
    }

    private string? GetValidNumeroCNH()
    {
        return Faker.Random.Replace("####################");
    }

    private string? GetValidCNPJ()
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