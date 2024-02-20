namespace HunterDomain.Extensions;

public struct Queries
{
    public Queries()
    {
    }

    #region Queries
    private string[] GetQueries = new string[] {
  
          /// <summary>Pos: 0 => QueryMotosAll</summary>
          @" SELECT * FROM ""Motos"";",

          /// <summary>Pos: 1 => QueryMotosById</summary>
          @"SELECT * FROM ""Motos"" Where ""Id""=@Id;",
  
          /// <summary>Pos: 2 => QuetyMotosPorPlaca</summary>
          @"SELECT * FROM ""Motos"" Where ""Placa""=@Placa;",

          /// <summary>Pos: 3 => QueryEntregadoresAll</summary>
          @"SELECT * FROM ""Entregadores"";",

          /// <summary>Pos: 4 => QueryEntregadorById</summary>
          @"SELECT * FROM ""Entregadores"" Where ""Id""=@Id;",
  
          /// <summary>Pos: 5 => QueryEntregadoresByNumberCNH</summary>
          @"SELECT * FROM ""Entregadores"" Where ""NumeroCNH""=@NumeroCNH;",

          /// <summary>Pos: 6 => QueryLocacoesAll</summary>
          @"SELECT * FROM ""Locacoes"";",

          /// <summary>Pos: 7 => QueryLocacaoById</summary>
          @"SELECT * FROM ""Locacoes"" Where ""Id""=@Id;",

          /// <summary>Pos: 8 => QueryPedidosAll</summary>
          @"SELECT * FROM ""Pedidos"";",

          /// <summary>Pos: 9 => QueryPedidoById</summary>
          @"SELECT * FROM ""Pedidos"" WHERE ""Id""=@Id;",

          /// <summary>Pos: 10 => QueryMotoPossuiLocacoes</summary>
          @"SELECT count(*) FROM ""Locacoes"" Where ""MotoId""=@Id;",

          /// <summary>Pos: 11 => QueryMotosDisponiveis</summary>
          @"SELECT * FROM ""Motos"" Left JOIN ""Locacoes"" ON ""Locacoes"".""MotoId"" = ""Motos"".""Id"" Where ""Locacoes"".""Status"" = 'Ativa';",

          /// <summary>Pos: 12 => QueryMotoDisponivelById</summary>
          @"SELECT count(*) FROM ""Motos"" Left JOIN ""Locacoes"" ON ""Locacoes"".""MotoId"" = ""Motos"".""Id"" Where ""Motos"".""Id""=@Id And ""Locacoes"".""Status"" = 'Ativa';",

          /// <summary>Pos: 13 => QueryNotificaoByEntregadorPedido</summary>
          @"SELECT * FROM ""Notificacoes"" Where ""EntregadorId""=@EntregadorId And ""PedidoId""=@PedidoId;",

          /// <summary>Pos: 14 => QueryEntregadorByNotificacao</summary>
          @"SELECT * FROM ""Notificacoes"" Inner Join ""Entregadores"" On ""Notificacoes"".""EntregadorId""=""Entregadores"".""Id"" Where ""Notificacoes"".""PedidoId""=@PedidoId;;",

          /// <summary>Pos: 15 => QueryEntregadoresDisponives</summary>
          @"SELECT ""Entregadores"".""Id"", ""Nome"", ""CNH"", ""CategoriaCNH"", ""CNPJ"", ""DataNascimento"", ""NumeroCNH"", ""Ativo"" FROM ""Entregadores"" JOIN ""Locacoes"" ON ""Entregadores"".""Id"" = ""Locacoes"".""EntregadorId"" LEFT JOIN ""Pedidos"" ON ""Entregadores"".""Id"" = ""Pedidos"". ""EntregadorId"" Where ""Locacoes"".""Status"" = 'Ativa' AND (""Pedidos"".""Status"" != 'Aceito' or ""Pedidos"".""Status"" is null);"

    };
    #endregion

    public string GetQuery(int index)
    {
        return GetQueries[index];
    }
}