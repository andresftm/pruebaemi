using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEmi.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        // Métodos que necesitarían por cada controladorer
        Task<T?> GetByIdAsync(int id);           // Obtener por ID
        Task<IEnumerable<T>> GetAllAsync();      // Obtener todos
        Task<T> AddAsync(T entity);              // Agregar
        Task UpdateAsync(T entity);              // Actualizar
        Task DeleteAsync(T entity);              // Eliminar
    }
}
