using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models.ViewModels
{
    public class BookVM
    {
        public Book Book {  get; set; }

        public IEnumerable<Book> Books { get; set; } = new List<Book>();

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; } 
    }
}
