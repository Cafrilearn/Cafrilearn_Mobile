using System.Collections.Generic;
using System.IO;

namespace AfriLearn.Models
{
    class BookDto
    {
        public  string  BookType { get; set; }
        public  List<string>  AllBookNames { get; set; }
        public  string  BookFormat { get; set; }
        public  Stream  BookStream { get; set; }
        public string BookName { get; set; }
    }
}
