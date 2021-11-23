using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReadingLog.Core
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public Status Status { get; set; }
        //public int AuthorId { get; set; }
        //public Author Author { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? DaysReading { get; set; } 
        public int Rating { get; set; }
    }
}