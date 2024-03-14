using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [DisplayName("Book Name")]
        public string BookName { get; set; }

        [Required]
        [DisplayName("Publish Year")]
        public int PublishYear { get; set; }

        [Required]
        [DisplayName("Cover Price")]
        public int CoverPrice { get; set; }

        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string Image { get; set; }
        public bool Status { get; set; }

    }
}
