using Books.DataAccessLayer.Data;
using Books.DataAccessLayer.Infrastructure.IRepository;
using Books.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.DataAccessLayer.Infrastructure.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {

        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Book book)
        {
            
            var bk = _context.Books.Where(x => x.BookId == book.BookId).FirstOrDefault();
            if(bk != null)
            {
                bk.BookName = book.BookName;
                bk.PublishYear = book.PublishYear;
                bk.CoverPrice = book.CoverPrice;
                bk.CategoryId = book.CategoryId;
                bk.Image = book.Image;
                bk.Status = book.Status;
                _context.Books.Update(bk);
            }
        }
    }
}
