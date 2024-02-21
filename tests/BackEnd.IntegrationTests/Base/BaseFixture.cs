using BackEnd.Infra.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace BackEnd.IntegrationTests.Base;

public class BaseFixture
{
	public BaseFixture()
		=> Faker = new Faker("pt_BR");

    protected Faker Faker { get; set; }

    public PgDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new PgDbContext(
            new DbContextOptionsBuilder<PgDbContext>()
            .UseInMemoryDatabase("integration-tests-db")
            .Options
        );

        if (preserveData == false)
            context.Database.EnsureDeleted();
        return context;
    }
}

