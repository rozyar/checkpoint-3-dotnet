using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private DbContextOptions<ApplicationContext> CreateNewContextOptions()
        {
            // Gera um novo banco de dados em memória para cada teste, garantindo isolamento.
            return new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: $"BarcoTestDb_{Guid.NewGuid()}")
                .Options;
        }

        [Fact]
        public void ObterTodos_ShouldReturnAllBarcos()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new ApplicationContext(options))
            {
                var barco1 = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
                var barco2 = new BarcoEntity { Nome = "Barco2", Modelo = "Modelo2", Ano = 2005, Tamanho = 12.0 };

                context.Barcos.AddRange(barco1, barco2);
                context.SaveChanges();
            }

            // Act
            using (var context = new ApplicationContext(options))
            {
                var barcoRepository = new BarcoRepository(context);
                var result = barcoRepository.ObterTodos();

                // Assert
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public void Adicionar_ShouldAddBarco()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new ApplicationContext(options))
            {
                var barcoRepository = new BarcoRepository(context);
                var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };

                // Act
                var result = barcoRepository.Adicionar(barco);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, context.Barcos.Count());
            }
        }

        [Fact]
        public void ObterPorId_ShouldReturnBarco()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new ApplicationContext(options))
            {
                var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
                context.Barcos.Add(barco);
                context.SaveChanges();

                // Act
                var barcoRepository = new BarcoRepository(context);
                var result = barcoRepository.ObterPorId(barco.Id);

                // Assert
                Assert.Equal(barco, result);
            }
        }

        [Fact]
        public void Editar_ShouldUpdateBarco()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new ApplicationContext(options))
            {
                var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
                context.Barcos.Add(barco);
                context.SaveChanges();

                barco.Nome = "Barco1Editado";

                // Act
                var barcoRepository = new BarcoRepository(context);
                var result = barcoRepository.Editar(barco);

                // Assert
                var updatedBarco = context.Barcos.Find(barco.Id);
                Assert.Equal("Barco1Editado", updatedBarco.Nome);
            }
        }

        [Fact]
        public void Remover_ShouldDeleteBarco()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new ApplicationContext(options))
            {
                var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
                context.Barcos.Add(barco);
                context.SaveChanges();

                // Act
                var barcoRepository = new BarcoRepository(context);
                var result = barcoRepository.Remover(barco.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(0, context.Barcos.Count());
            }
        }
    }
}
