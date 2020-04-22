using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenAccess<T>
    {
        void Create(T obj);
        T Read(int key);
        void Update(T obj);
        void Delete(int key);

    }
}
