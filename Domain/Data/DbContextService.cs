using Domain.Abstractions.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace Domain.Data
{

    public class DbContextService : DbContext, IDbContext
    {
        public DbContextService(DbContextOptions<DbContextService> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Prato> Pratos { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurante>().HasKey(r => r.Id);
            modelBuilder.Entity<Prato>().HasKey(p => p.Id);
            modelBuilder.Entity<Restaurante>().HasData(
                new Restaurante { Id = 1, Nome = "Aluroni" },
                new Restaurante { Id = 2, Nome = "Hamburgueria Lura" },
                new Restaurante { Id = 3, Nome = "Obi-Wan Kenobi Sushi" },
                new Restaurante { Id = 4, Nome = "Neo Trinity Vegan Foods" },
                new Restaurante { Id = 5, Nome = "Lyllys Cafe" },
                new Restaurante { Id = 6, Nome = "Sugiro Sushi" },
                new Restaurante { Id = 7, Nome = "Cantina da escola" },
                new Restaurante { Id = 8, Nome = "Lanchonete Agarikov" }
            );

            modelBuilder.Entity<Prato>().HasData(
                new Prato
                {
                    Id = 1,
                    Nome = "Pizza Toscana",
                    Tag = "Italiana",
                    Imagem = "img.jpeg",
                    Descricao = "Mais uma tradição da Itália, a Toscana da Rede Leve Pizza com muita calabresa ralada e bacon vai levar um sabor diferenciado para sua mesa.",
                    RestauranteId = 1
                },
                new Prato
                {
                    Id = 2,
                    Nome = "Macarrão",
                    Tag = "Massas",
                    Imagem = "img.jpeg",
                    Descricao = "Carbonara, é uma receita tradicional italiana de massa. Acredita-se que tenha sido uma receita apreciada pelos preparadores de carvão vegetal (carbinai) dos montes apeninos, na região italiana da Úmbria.",
                    RestauranteId = 1
                },
                new Prato
                {
                    Id = 3,
                    Nome = "X Salada",
                    Tag = "Americana",
                    Imagem = "img.jpeg",
                    Descricao = "Sanduíche com hambúrguer e queijo acompanhado com salada.",
                    RestauranteId = 2
                },
                new Prato
                {
                    Id = 4,
                    Nome = "X Tudo",
                    Tag = "Americana",
                    Imagem = "img.jpeg",
                    Descricao = "Sanduíche com muita carne e tudo que tiver disponível na cozinha.",
                    RestauranteId = 2
                },
                new Prato
                {
                    Id = 5,
                    Nome = "Sushi",
                    Tag = "Japonesa",
                    Imagem = "img.jpeg",
                    Descricao = "Iguaria de origem japonesa, caracterizado por ser feito de arroz temperado, enrolado em peixe cru ou frutos do mar frescos, vegetais, frutas, ovos e algas.",
                    RestauranteId = 3
                },
                new Prato
                {
                    Id = 6,
                    Nome = "Temaki",
                    Tag = "Japonesa",
                    Imagem = "img.jpeg",
                    Descricao = "O clássico arroz japonês acompanhado de peixes, alga marinha ou verduras ganhou o paladar do brasileiro.",
                    RestauranteId = 3
                },
                new Prato
                {
                    Id = 7,
                    Nome = "Caldo",
                    Tag = "Caldos",
                    Imagem = "img.jpeg",
                    Descricao = "A sopa vai para o prato com pedaços de legumes, hortaliças ou carnes. Também pode entrar o arroz ou o macarrão,ingredientes que dão sustança.",
                    RestauranteId = 4
                },
                new Prato
                {
                    Id = 8,
                    Nome = "Feijoada Smith",
                    Tag = "Mineira",
                    Imagem = "img.jpeg",
                    Descricao = "A feijoada é um dos pratos típicos mais conhecidos e populares da culinária brasileira.",
                    RestauranteId = 4
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
