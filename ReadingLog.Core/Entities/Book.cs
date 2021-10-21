using System;

namespace ReadingLog.Core
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Status Status { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public TimeSpan DaysReading { get; set; } 
    }
}