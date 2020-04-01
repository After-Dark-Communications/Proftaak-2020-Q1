using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenAccess<T>
    {
        Task Create(T obj);
        T Read(int key);
        Task Update(T obj);
        Task Delete(int key);

    }
}
