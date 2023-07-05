using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoglass.Application.Interface
{
    public interface IApplication<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Create(T entidade);
        Task<bool> Delete(int Id);
        Task<bool> Update(T entidade);
    }
}
