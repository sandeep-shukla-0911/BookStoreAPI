﻿namespace BookStore.Models
{
    public class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Language { get; set; }
        public double? Price { get; set; }
    }
}
