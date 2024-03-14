using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccessLayer.Infrastructure.IRepository
{
    public interface IUnitOfWork
    {

        IBookRepository Books {  get; }
        ICategoryRepository Categories { get; }

        void Save();

    }
}
