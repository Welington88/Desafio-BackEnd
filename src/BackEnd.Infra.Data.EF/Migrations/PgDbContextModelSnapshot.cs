﻿// <auto-generated />
using System;
using BackEnd.Infra.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd.Infra.Data.EF.Migrations
{
    [DbContext(typeof(PgDbContext))]
    partial class PgDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackEnd.Domain.Entities.Entregador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("CNH")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("CNPJ")
                        .HasColumnType("text");

                    b.Property<string>("CategoriaCNH")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("character varying(2)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("date");

                    b.Property<Guid?>("LocacaoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<Guid?>("NotificacaoId")
                        .HasColumnType("uuid");

                    b.Property<string>("NumeroCNH")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid?>("PedidoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CNPJ")
                        .IsUnique();

                    b.HasIndex("LocacaoId");

                    b.HasIndex("NotificacaoId");

                    b.HasIndex("NumeroCNH")
                        .IsUnique();

                    b.HasIndex("PedidoId");

                    b.ToTable("Entregadores");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f1f6f4dd-0998-4149-8b60-0055eeec24cd"),
                            Ativo = true,
                            CNH = "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                            CNPJ = "10.029.0190/0001-90",
                            CategoriaCNH = "AB",
                            DataNascimento = new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nome = "José Antonio",
                            NumeroCNH = "92385235"
                        },
                        new
                        {
                            Id = new Guid("c8fbc31b-2265-4b77-a4c3-73eea103bd0d"),
                            Ativo = true,
                            CNH = "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                            CNPJ = "10.029.0190/0001-89",
                            CategoriaCNH = "AB",
                            DataNascimento = new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nome = "Pedro Antonio",
                            NumeroCNH = "92385234"
                        },
                        new
                        {
                            Id = new Guid("9ec54dc0-d8ab-4c6b-b04e-3147a11bfafd"),
                            Ativo = true,
                            CNH = "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                            CNPJ = "10.029.0190/0001-91",
                            CategoriaCNH = "AB",
                            DataNascimento = new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nome = "Maria José",
                            NumeroCNH = "92385233"
                        },
                        new
                        {
                            Id = new Guid("d87c0305-70e3-4a1d-ae2f-b98878010528"),
                            Ativo = true,
                            CNH = "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp",
                            CNPJ = "10.029.0190/0001-92",
                            CategoriaCNH = "AB",
                            DataNascimento = new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nome = "Antonio Pedro",
                            NumeroCNH = "92385232"
                        });
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Locacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataPrevistaTermino")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataTermino")
                        .HasColumnType("date");

                    b.Property<Guid?>("EntregadorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MotoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Plano")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("PrazoEmDias")
                        .HasColumnType("integer")
                        .HasAnnotation("Range", new[] { 1, 2147483647 });

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<decimal?>("ValorAdicional")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ValorDiaria")
                        .HasColumnType("numeric")
                        .HasAnnotation("Range", new[] { 1, 2147483647 });

                    b.Property<decimal?>("ValorMulta")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("numeric")
                        .HasAnnotation("Range", new[] { 1, 2147483647 });

                    b.HasKey("Id");

                    b.ToTable("Locacoes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3f104368-957e-40d1-801e-f1058dbf2183"),
                            DataCriacao = new DateTime(2024, 2, 19, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2250),
                            DataInicio = new DateTime(2024, 2, 20, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2300),
                            DataPrevistaTermino = new DateTime(2024, 2, 26, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2300),
                            DataTermino = new DateTime(2024, 2, 26, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2300),
                            Plano = "7Dias",
                            PrazoEmDias = 7,
                            Status = "Ativo",
                            ValorDiaria = 30m,
                            ValorTotal = 210m
                        });
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Moto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Ano")
                        .HasColumnType("integer");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LocacaoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.HasIndex("LocacaoId");

                    b.HasIndex("Placa")
                        .IsUnique();

                    b.ToTable("Motos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3a8707ae-617e-4c9c-acd6-8cd0b855abcb"),
                            Ano = 2014,
                            Ativo = true,
                            Modelo = "XRE",
                            Placa = "RVG-0J83"
                        },
                        new
                        {
                            Id = new Guid("c08e1b10-b53a-469d-8236-079f8259b10f"),
                            Ano = 2015,
                            Ativo = true,
                            Modelo = "CG 160 FAN",
                            Placa = "RVG-0J84"
                        },
                        new
                        {
                            Id = new Guid("2e1a7ac9-6134-4dea-982d-c9c2051327cc"),
                            Ano = 2008,
                            Ativo = true,
                            Modelo = "CG 150 START",
                            Placa = "RVG-0J82"
                        },
                        new
                        {
                            Id = new Guid("e9fc6772-ebfc-44c1-ba1a-6bc05ca1ebee"),
                            Ano = 2021,
                            Ativo = true,
                            Modelo = "XRE",
                            Placa = "RVG-0J81"
                        },
                        new
                        {
                            Id = new Guid("874f1665-a264-4fd3-9809-495ea35583eb"),
                            Ano = 2019,
                            Ativo = true,
                            Modelo = "CG 150 TITAN",
                            Placa = "RVG-0J80"
                        });
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Notificacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataNoticacao")
                        .HasColumnType("date");

                    b.Property<Guid>("EntregadorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Notificacoes");
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("date");

                    b.Property<Guid?>("EntregadorId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("NotificacaoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<decimal>("ValorDaCorrida")
                        .HasColumnType("numeric")
                        .HasAnnotation("Range", new[] { 1, 2147483647 });

                    b.HasKey("Id");

                    b.HasIndex("NotificacaoId");

                    b.ToTable("Pedidos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0fa34707-bb03-448b-8753-2550f18f1204"),
                            DataCriacao = new DateTime(2024, 2, 19, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2660),
                            Status = "Disponível",
                            ValorDaCorrida = 87m
                        });
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Entregador", b =>
                {
                    b.HasOne("BackEnd.Domain.Entities.Locacao", "Locacao")
                        .WithMany("Entregadores")
                        .HasForeignKey("LocacaoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BackEnd.Domain.Entities.Notificacao", null)
                        .WithMany("Entregadores")
                        .HasForeignKey("NotificacaoId");

                    b.HasOne("BackEnd.Domain.Entities.Pedido", null)
                        .WithMany("Entregadores")
                        .HasForeignKey("PedidoId");

                    b.Navigation("Locacao");
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Moto", b =>
                {
                    b.HasOne("BackEnd.Domain.Entities.Locacao", "Locacao")
                        .WithMany("Motos")
                        .HasForeignKey("LocacaoId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Locacao");
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Pedido", b =>
                {
                    b.HasOne("BackEnd.Domain.Entities.Notificacao", null)
                        .WithMany("Pedidos")
                        .HasForeignKey("NotificacaoId");
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Locacao", b =>
                {
                    b.Navigation("Entregadores");

                    b.Navigation("Motos");
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Notificacao", b =>
                {
                    b.Navigation("Entregadores");

                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("BackEnd.Domain.Entities.Pedido", b =>
                {
                    b.Navigation("Entregadores");
                });
#pragma warning restore 612, 618
        }
    }
}
