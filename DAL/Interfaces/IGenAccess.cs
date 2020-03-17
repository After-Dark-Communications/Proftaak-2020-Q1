using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    interface IGenAccess<T>
    {
        void Create(T obj);
        void Read(int key);
        void Update(T obj);
        void Delete(int key);

    }
}
