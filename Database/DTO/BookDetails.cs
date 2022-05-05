using System.Collections.Generic;

namespace Database.DTO
{
    public class BookDetails
    {
        public int BookId { get; set; }

        public string Titile { get; set; }

        public List<string> Authors { get; set; }
    }
}