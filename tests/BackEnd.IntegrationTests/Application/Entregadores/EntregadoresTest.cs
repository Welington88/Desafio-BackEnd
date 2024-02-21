using BackEnd.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.IntegrationTests.Application;

[Collection(nameof(EntregadoresTestFixture))]
public class EntregadoresTest
{
    private readonly EntregadoresTestFixture _fixture;

    public EntregadoresTest(EntregadoresTestFixture fixture)
        => _fixture = fixture;
    
    [Fact(DisplayName = nameof(InsertEntregador))]
    [Trait("Integration/Api -> Domain", "Entregador - DataContext")]
    public async Task InsertEntregador()
    {
        var ValidListEntregadores = _fixture.GetListValidEntregadores();
        var _dbContext = _fixture.CreateDbContext(true);

        foreach (var validEntregador in ValidListEntregadores)
        {
            var entregador = await _dbContext.Entregadores.AddAsync(validEntregador);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            entregador.Entity.Should().NotBeNull();
            entregador.Entity.CNPJ.Should().Be(validEntregador.CNPJ);
            entregador.Entity.CNPJ.Should().NotBeNull();
            (entregador.Entity.CNPJ!.Length <= 20).Should().BeTrue();
            entregador.Entity.NumeroCNH.Should().Be(validEntregador.NumeroCNH);
            entregador.Entity.NumeroCNH.Should().NotBeNull();
            (entregador.Entity.NumeroCNH!.Length <= 20).Should().BeTrue();
            entregador.Entity.CategoriaCNH.Should().Be(validEntregador.CategoriaCNH);
            entregador.Entity.CategoriaCNH.Should().NotBeNull();
            (entregador.Entity.CategoriaCNH!.Length <= 2).Should().BeTrue();
            entregador.Entity.DataNascimento.Should().Be(validEntregador.DataNascimento);
            entregador.Entity.DataNascimento.Should().NotBe(default(DateTime));
            entregador.Entity.Ativo.Should().Be(validEntregador.Ativo);
        }
    }

    [Fact(DisplayName = nameof(UpdateEntregador))]
    [Trait("Integration/Api -> Domain", "Entregador - DataContext")]
    public async Task UpdateEntregador()
    {
        var ValidEntregadoresInsert = _fixture.GetValidEntregador();
        var _dbContext = _fixture.CreateDbContext(true);
    
        await _dbContext.Entregadores.AddAsync(ValidEntregadoresInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var validEntregador = await _dbContext.Entregadores.FindAsync(ValidEntregadoresInsert.Id);

        validEntregador!.CNPJ = _fixture.GetValidCNPJ();
        validEntregador!.NumeroCNH = _fixture.GetValidNumeroCNH();
        validEntregador!.CategoriaCNH = _fixture.GetValidValidCategoriaCNH();
        validEntregador.DataNascimento = _fixture.GetValidValidDataNascimento();
        validEntregador.CNH = _fixture.GetValidValidImagemCNH();


        _dbContext.Entregadores.Update(validEntregador!);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var entregador = await _dbContext.Entregadores.FindAsync(validEntregador.Id);
        entregador.Should().NotBeNull();
        entregador!.CNPJ.Should().Be(validEntregador.CNPJ);
        entregador.CNPJ.Should().NotBeNull();
        (entregador.CNPJ!.Length <= 20).Should().BeTrue();
        entregador.NumeroCNH.Should().Be(validEntregador.NumeroCNH);
        entregador.NumeroCNH.Should().NotBeNull();
        (entregador.NumeroCNH!.Length <= 20).Should().BeTrue();
        entregador.CategoriaCNH.Should().Be(validEntregador.CategoriaCNH);
        entregador.CategoriaCNH.Should().NotBeNull();
        (entregador.CategoriaCNH!.Length <= 2).Should().BeTrue();
        entregador.DataNascimento.Should().Be(validEntregador.DataNascimento);
        entregador.DataNascimento.Should().NotBe(default(DateTime));
        entregador.Ativo.Should().Be(validEntregador.Ativo);
    }

    [Fact(DisplayName = nameof(InactivateEntregador))]
    [Trait("Integration/Api -> Domain", "Entregador - DataContext")]
    public async Task InactivateEntregador()
    {
        var ValidListEntregadoresInsert = _fixture.GetListValidEntregadores();
        var _dbContext = _fixture.CreateDbContext(true);

        await _dbContext.Entregadores.AddRangeAsync(ValidListEntregadoresInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var ValidListEntregadoresInactivate = await _dbContext.Entregadores.ToListAsync();

        ValidListEntregadoresInactivate.Should().HaveCount(ValidListEntregadoresInactivate.Count);
        _dbContext.Entregadores.RemoveRange(ValidListEntregadoresInactivate);

        ValidListEntregadoresInactivate.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(FindAllEntregadores))]
    [Trait("Integration/Api -> Domain", "Entregadores - DataContext")]
    public async Task FindAllEntregadores()
    {
        var ValidListEntregadoresInsert = _fixture.GetListValidEntregadores();
        var _dbContext = _fixture.CreateDbContext();

        await _dbContext.Entregadores.AddRangeAsync(ValidListEntregadoresInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var ValidListEntregadoresUpdate = await _dbContext.Entregadores.ToListAsync();

        ValidListEntregadoresUpdate.Should().HaveCount(ValidListEntregadoresUpdate.Count);
        ValidListEntregadoresUpdate.ForEach(UpdateEntregador =>{
            var UpdateResult = ValidListEntregadoresInsert.Where(c => c.Id == UpdateEntregador.Id).FirstOrDefault();
            UpdateResult.Should().NotBeNull();
        });

        _dbContext = _fixture.CreateDbContext(true);
    }
}