using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ReadingLog.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string Title { get; set; }
        public Status Status { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? DaysReading { get; set; } 
        public int Rating { get; set; }
    }
}