using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BarcoEntity> ObterTodosBarcos()
        {
            return _repository.ObterTodos();
        }

        public BarcoEntity ObterBarcoPorId(int id)
        {
            return _repository.ObterPorId(id);
        }

        public BarcoEntity AdicionarBarco(IBarcoDto dto)
        {
            dto.Validate();

            var barcoEntity = new BarcoEntity
            {
                Nome = dto.Nome,
                Modelo = dto.Modelo,
                Ano = dto.Ano,
                Tamanho = dto.Tamanho
            };

            return _repository.Adicionar(barcoEntity);
        }

        public BarcoEntity EditarBarco(int id, IBarcoDto dto)
        {
            dto.Validate();

            var barcoEntity = _repository.ObterPorId(id);
            if (barcoEntity == null)
                throw new Exception("Barco não encontrado");

            barcoEntity.Nome = dto.Nome;
            barcoEntity.Modelo = dto.Modelo;
            barcoEntity.Ano = dto.Ano;
            barcoEntity.Tamanho = dto.Tamanho;

            return _repository.Editar(barcoEntity);
        }

        public BarcoEntity RemoverBarco(int id)
        {
            var barco = _repository.Remover(id);
            if (barco == null)
                throw new Exception("Barco não encontrado");

            return barco;
        }
    }
}
