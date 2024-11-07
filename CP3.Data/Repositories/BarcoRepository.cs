using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public BarcoEntity? ObterPorId(int id)
        {
            return _context.Barcos.Find(id);
        }

        public IEnumerable<BarcoEntity>? ObterTodos()
        {
            return _context.Barcos.ToList();
        }

        public BarcoEntity? Adicionar(BarcoEntity barco)
        {
            _context.Barcos.Add(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Editar(BarcoEntity barco)
        {
            _context.Barcos.Update(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Remover(int id)
        {
            var barco = _context.Barcos.Find(id);
            if (barco != null)
            {
                _context.Barcos.Remove(barco);
                _context.SaveChanges();
            }
            return barco;
        }
    }
}
