using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class Author
    {
        public Author()
        {
            this.DocumentAuthors = new List<DocumentAuthor>();
        }

        public int AuthorID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DocumentAuthor> DocumentAuthors { get; set; }
    }
}
