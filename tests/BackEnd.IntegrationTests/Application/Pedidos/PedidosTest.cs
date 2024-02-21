using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.IntegrationTests.Application;

[Collection(nameof(PedidosTestFixture))]
public class PedidosTest
{
    private readonly PedidosTestFixture _fixture;

    public PedidosTest(PedidosTestFixture fixture)
        => _fixture = fixture;
    
    [Fact(DisplayName = nameof(InsertPedido))]
    [Trait("Integration/Api -> Domain", "Pedido - DataContext")]
    public async Task InsertPedido()
    {
        var ValidListPedidos = _fixture.GetListValidPedidos();
        var _dbContext = _fixture.CreateDbContext(true);

        foreach (var ValidPedido in ValidListPedidos)
        {
            var dbContextReturn = await _dbContext.Pedidos.AddAsync(ValidPedido);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            dbContextReturn.Entity.Should().NotBeNull();
            dbContextReturn.Entity.Id.Should().Be(ValidPedido.Id);
            dbContextReturn.Entity.Id.Should().NotBe(default(Guid));
            (dbContextReturn.Entity.ValorDaCorrida > 0).Should().BeTrue();
            (dbContextReturn.Entity.ValorDaCorrida <= 0).Should().BeFalse();
            dbContextReturn.Entity.Status.Should().Be(ValidPedido.Status);
            dbContextReturn.Entity.Status.Should().NotBeNullOrWhiteSpace();
            (dbContextReturn.Entity.Status!.Length <= 10).Should().BeTrue();
            dbContextReturn.Entity.DataCriacao.Should().Be(ValidPedido.DataCriacao);
            dbContextReturn.Entity.DataCriacao.Should().NotBe(default(DateTime));
            dbContextReturn.Entity.EntregadorId.Should().NotBe(default(Guid)); 
        }
    }

    [Fact(DisplayName = nameof(UpdatePedido))]
    [Trait("Integration/Api -> Domain", "Pedido - DataContext")]
    public async Task UpdatePedido()
    {
        var ValidPedidosInsert = _fixture.GetValidPedido();
        var _dbContext = _fixture.CreateDbContext(true);
    
        await _dbContext.Pedidos.AddAsync(ValidPedidosInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var ValidPedido = await _dbContext.Pedidos.FindAsync(ValidPedidosInsert.Id);

        ValidPedido!.ValorDaCorrida = _fixture.GetValidValidDecimal();
        ValidPedido!.DataCriacao = _fixture.GetValidDate();
        ValidPedido!.EntregadorId = _fixture.GetValidGuid();
        ValidPedido.Status = _fixture.GetValidStatus();


        _dbContext.Pedidos.Update(ValidPedido);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var dbContextReturn = await _dbContext.Pedidos.FindAsync(ValidPedido.Id);
        dbContextReturn.Should().NotBeNull();
        dbContextReturn!.Id.Should().Be(ValidPedido.Id);
        dbContextReturn.Id.Should().NotBe(default(Guid));
        (dbContextReturn.ValorDaCorrida > 0).Should().BeTrue();
        (dbContextReturn.ValorDaCorrida <= 0).Should().BeFalse();
        dbContextReturn.Status.Should().Be(ValidPedido.Status);
        dbContextReturn.Status.Should().NotBeNullOrWhiteSpace();
        (dbContextReturn.Status!.Length <= 10).Should().BeTrue();
        dbContextReturn.DataCriacao.Should().Be(ValidPedido.DataCriacao);
        dbContextReturn.DataCriacao.Should().NotBe(default(DateTime));
        dbContextReturn.EntregadorId.Should().NotBe(default(Guid));
    }

    [Fact(DisplayName = nameof(InactivatePedido))]
    [Trait("Integration/Api -> Domain", "Pedido - DataContext")]
    public async Task InactivatePedido()
    {
        var ValidListPedidosInsert = _fixture.GetListValidPedidos();
        var _dbContext = _fixture.CreateDbContext(true);

        await _dbContext.Pedidos.AddRangeAsync(ValidListPedidosInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);

        var ValidListPedidosInactivate = await _dbContext.Pedidos.ToListAsync();

        ValidListPedidosInactivate.Should().HaveCount(ValidListPedidosInactivate.Count);
        _dbContext.Pedidos.RemoveRange(ValidListPedidosInactivate);

        ValidListPedidosInactivate.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(FindAllPedidos))]
    [Trait("Integration/Api -> Domain", "Pedidos - DataContext")]
    public async Task FindAllPedidos()
    {
        var ValidListPedidosInsert = _fixture.GetListValidPedidos();
        var _dbContext = _fixture.CreateDbContext();

        await _dbContext.Pedidos.AddRangeAsync(ValidListPedidosInsert);
        await _dbContext.SaveChangesAsync(CancellationToken.None);
        var ValidListPedidosUpdate = await _dbContext.Pedidos.ToListAsync();

        ValidListPedidosUpdate.Should().HaveCount(ValidListPedidosUpdate.Count);
        ValidListPedidosUpdate.ForEach(UpdatePedido =>{
            var UpdateResult = ValidListPedidosInsert.Where(c => c.Id == UpdatePedido.Id).FirstOrDefault();
            UpdateResult.Should().NotBeNull();
        });

        _dbContext = _fixture.CreateDbContext(true);
    }
}