using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccessLayer.Infrastructure.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {

        void Update(Book book);

    }
}
