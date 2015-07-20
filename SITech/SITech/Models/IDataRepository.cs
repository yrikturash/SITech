using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SITech.DTO;

namespace SITech.Models
{
    interface IDataRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}