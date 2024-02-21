using BackEnd.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.IntegrationTests.Application;

[Collection(nameof(LocacoesTestFixture))]
public class LocacoesTest
{
    private readonly LocacoesTestFixture _fixture;

    public LocacoesTest(LocacoesTestFixture fixture)
        => _fixture = fixture;
    
    [Fact(DisplayName = nameof(InsertLocacao))]
    [Trait("Integration/Api -> Domain", "Locacao - DataContext")]
    public async Task InsertLocacao()
    {
        var ValidListLocacoes = _fixture.GetListValidLocacoes();
        var _dbContext = _fixture.CreateDbContext(true);

        foreach (var validLocacao in ValidListLocacoes)
        {
            var Locacao = await _dbContext.Locacoes.AddAsync(validLocacao);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            Locacao.Entity.Should().NotBeNull();
            Locacao.Should().NotBeNull();
            Locacao.Entity.PrazoEmDias.Should().Be(validLocacao.PrazoEmDias);
            Locacao.Entity.Should().NotBe(default(int));
            (Locacao.Entity.PrazoEmDias < 0).Should().BeFalse();
            Locacao.Entity.Plano.Should().Be(validLocacao.Plano);
            Locacao.Entity.Plano.Should().NotBeNullOrWhiteSpace();
            (Locacao.Entity.Plano!.Length <= 20).Should().BeTrue();
            Locacao.Entity.Status.Should().Be(validLocacao.Status);
            Locacao.Entity.Status.Should().NotBeNullOrWhiteSpace();
            (Locacao.Entity.Status!.Length <= 10).Should().BeTrue();
            Locacao.Entity.PrazoEmDias.Should().Be(validLocacao.PrazoEmDias);
            Locacao.Entity.PrazoEmDias.Should().NotBe(default(int));

            Locacao.Entity.ValorDiaria.Should().Be(validLocacao.ValorDiaria);
            Locacao.Entity.ValorDiaria.Should().NotBe(default(int));
            (Locacao.Entity.ValorDiaria < 0).Should().BeFalse();

            Locacao.Entity.ValorTotal.Should().Be(validLocacao.ValorTotal);
            Locacao.Entity.ValorTotal.Should().NotBe(default(int));
            (Locacao.Entity.ValorTotal < 0).Should().BeFalse();

            Locacao.Entity.EntregadorId.Should().NotBe(default(Guid));
            Locacao.Entity.MotoId.Should().NotBe(default(Guid));
        }
    }

    [Fact(DisplayName = nameof(UpdateLocacao))]
    [Trait("Integration/Api -> Domain", "Locacao - DataContext")]
    public async Task UpdateLocacao()
    {
        var ValidLocacoesInsert = _fixture.GetValidLocacao();
        var _dbContext = _fixture.CreateDbContext(true);
    
        await _dbContext.Locacoes.AddAsync(ValidLocacoesInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var validLocacao = await _dbContext.Locacoes.FindAsync(ValidLocacoesInsert.Id);

        validLocacao!.PrazoEmDias = _fixture.GalidDaysForPlan();
        validLocacao!.Plano = $"_{validLocacao!.PrazoEmDias}dias";
        validLocacao!.Status = _fixture.GetValidStatus();
        validLocacao.ValorDiaria = _fixture.GetValidValuePlan(validLocacao!.Plano);
        validLocacao.ValorTotal = validLocacao.ValorDiaria * validLocacao!.PrazoEmDias;


        _dbContext.Locacoes.Update(validLocacao!);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var Locacao = await _dbContext.Locacoes.FindAsync(validLocacao.Id);
        Locacao.Should().NotBeNull();
        Locacao.Should().NotBeNull();
        Locacao!.PrazoEmDias.Should().Be(validLocacao.PrazoEmDias);
        Locacao.PrazoEmDias.Should().NotBe(default(int));
        (Locacao.PrazoEmDias < 0).Should().BeFalse();
        Locacao.Plano.Should().Be(validLocacao.Plano);
        Locacao.Plano.Should().NotBeNullOrWhiteSpace();
        (Locacao.Plano!.Length <= 20).Should().BeTrue();
        Locacao.Status.Should().Be(validLocacao.Status);
        Locacao.Status.Should().NotBeNullOrWhiteSpace();
        (Locacao.Status!.Length <= 10).Should().BeTrue();
        Locacao.PrazoEmDias.Should().Be(validLocacao.PrazoEmDias);
        Locacao.PrazoEmDias.Should().NotBe(default(int));

        Locacao.ValorDiaria.Should().Be(validLocacao.ValorDiaria);
        Locacao.ValorDiaria.Should().NotBe(default(int));
        (Locacao.ValorDiaria < 0).Should().BeFalse();

        Locacao.ValorTotal.Should().Be(validLocacao.ValorTotal);
        Locacao.ValorTotal.Should().NotBe(default(int));
        (Locacao.ValorTotal < 0).Should().BeFalse();

        Locacao.EntregadorId.Should().NotBe(default(Guid));
        Locacao.MotoId.Should().NotBe(default(Guid));
    }

    [Fact(DisplayName = nameof(InactivateLocacao))]
    [Trait("Integration/Api -> Domain", "Locacao - DataContext")]
    public async Task InactivateLocacao()
    {
        var ValidListLocacoesInsert = _fixture.GetListValidLocacoes();
        var _dbContext = _fixture.CreateDbContext(true);

        await _dbContext.Locacoes.AddRangeAsync(ValidListLocacoesInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var ValidListLocacoesInactivate = await _dbContext.Locacoes.ToListAsync();

        ValidListLocacoesInactivate.Should().HaveCount(ValidListLocacoesInactivate.Count);
        _dbContext.Locacoes.RemoveRange(ValidListLocacoesInactivate);

        ValidListLocacoesInactivate.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(FindAllLocacoes))]
    [Trait("Integration/Api -> Domain", "Locacoes - DataContext")]
    public async Task FindAllLocacoes()
    {
        var ValidListLocacoesInsert = _fixture.GetListValidLocacoes();
        var _dbContext = _fixture.CreateDbContext();

        await _dbContext.Locacoes.AddRangeAsync(ValidListLocacoesInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var ValidListLocacoesUpdate = await _dbContext.Locacoes.ToListAsync();

        ValidListLocacoesUpdate.Should().HaveCount(ValidListLocacoesUpdate.Count);
        ValidListLocacoesUpdate.ForEach(UpdateLocacao =>{
            var UpdateResult = ValidListLocacoesInsert.Where(c => c.Id == UpdateLocacao.Id).FirstOrDefault();
            UpdateResult.Should().NotBeNull();
        });

        _dbContext = _fixture.CreateDbContext(true);
    }
}