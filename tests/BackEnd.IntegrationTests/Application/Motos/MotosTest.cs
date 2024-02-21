using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.IntegrationTests.Application;

[Collection(nameof(MotosTestFixture))]
public class MotosTest
{
    private readonly MotosTestFixture _fixture;

    public MotosTest(MotosTestFixture fixture)
        => _fixture = fixture;
    
    [Fact(DisplayName = nameof(InsertMoto))]
    [Trait("Integration/Api -> Domain", "Moto - DataContext")]
    public async Task InsertMoto()
    {
        var ValidListMotos = _fixture.GetListValidMotos();
        var _dbContext = _fixture.CreateDbContext(true);

        foreach (var ValidMoto in ValidListMotos)
        {
            var dbContextReturn = await _dbContext.Motos.AddAsync(ValidMoto);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            dbContextReturn.Entity.Should().NotBeNull();
            dbContextReturn.Entity.Id.Should().Be(ValidMoto.Id);
            dbContextReturn.Entity.Id.Should().NotBe(default(Guid));
            (dbContextReturn.Entity.Ano >= 1970).Should().BeTrue();
            (dbContextReturn.Entity.Ano < 0).Should().BeFalse();
            dbContextReturn.Entity.Modelo.Should().Be(ValidMoto.Modelo);
            dbContextReturn.Entity.Modelo.Should().NotBeNullOrWhiteSpace();
            (dbContextReturn.Entity.Modelo!.Length <= 50).Should().BeTrue();
            dbContextReturn.Entity.Placa.Should().Be(ValidMoto.Placa);
            dbContextReturn.Entity.Placa.Should().NotBeNullOrWhiteSpace();
            (dbContextReturn.Entity.Placa!.Length <= 10).Should().BeTrue();
            dbContextReturn.Entity.Ativo.Should().Be(ValidMoto.Ativo);   
        }
    }

    [Fact(DisplayName = nameof(UpdateMoto))]
    [Trait("Integration/Api -> Domain", "Moto - DataContext")]
    public async Task UpdateMoto()
    {
        var ValidMotosInsert = _fixture.GetValidMoto();
        var _dbContext = _fixture.CreateDbContext(true);
    
        await _dbContext.Motos.AddAsync(ValidMotosInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var ValidMoto = await _dbContext.Motos.FindAsync(ValidMotosInsert.Id);

        ValidMoto!.Ano = _fixture.GetValidYear();
        ValidMoto!.Modelo = _fixture.GetValidModel();
        ValidMoto!.Placa = _fixture.GetValidValidNumberPlate();
        ValidMoto.Ativo = _fixture.GetValidStatus();


        _dbContext.Motos.Update(ValidMoto);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var dbContextReturn = await _dbContext.Motos.FindAsync(ValidMoto.Id);
        dbContextReturn.Should().NotBeNull();
        dbContextReturn!.Id.Should().Be(ValidMoto.Id);
        dbContextReturn.Id.Should().NotBe(default(Guid));
        (dbContextReturn.Ano >= 1970).Should().BeTrue();
        (dbContextReturn.Ano < 0).Should().BeFalse();
        dbContextReturn.Modelo.Should().Be(ValidMoto.Modelo);
        dbContextReturn.Modelo.Should().NotBeNullOrWhiteSpace();
        (dbContextReturn.Modelo!.Length <= 50).Should().BeTrue();
        dbContextReturn.Placa.Should().Be(ValidMoto.Placa);
        dbContextReturn.Placa.Should().NotBeNullOrWhiteSpace();
        (dbContextReturn.Placa!.Length <= 10).Should().BeTrue();
        dbContextReturn.Ativo.Should().Be(ValidMoto.Ativo);
    }

    [Fact(DisplayName = nameof(InactivateMoto))]
    [Trait("Integration/Api -> Domain", "Moto - DataContext")]
    public async Task InactivateMoto()
    {
        var ValidListMotosInsert = _fixture.GetListValidMotos();
        var _dbContext = _fixture.CreateDbContext(true);

        await _dbContext.Motos.AddRangeAsync(ValidListMotosInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var ValidListMotosInactivate = await _dbContext.Motos.ToListAsync();

        ValidListMotosInactivate.Should().HaveCount(ValidListMotosInactivate.Count);
        _dbContext.Motos.RemoveRange(ValidListMotosInactivate);

        ValidListMotosInactivate.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(FindAllMotos))]
    [Trait("Integration/Api -> Domain", "Motos - DataContext")]
    public async Task FindAllMotos()
    {
        var ValidListMotosInsert = _fixture.GetListValidMotos();
        var _dbContext = _fixture.CreateDbContext();

        await _dbContext.Motos.AddRangeAsync(ValidListMotosInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var ValidListMotosUpdate = await _dbContext.Motos.ToListAsync();

        ValidListMotosUpdate.Should().HaveCount(ValidListMotosUpdate.Count);
        ValidListMotosUpdate.ForEach(UpdateMoto =>{
            var UpdateResult = ValidListMotosInsert.Where(c => c.Id == UpdateMoto.Id).FirstOrDefault();
            UpdateResult.Should().NotBeNull();
        });

        _dbContext = _fixture.CreateDbContext(true);
    }
}