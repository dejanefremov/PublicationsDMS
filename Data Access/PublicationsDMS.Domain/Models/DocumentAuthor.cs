using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class DocumentAuthor
    {
        public int DocumentAuthorID { get; set; }
        public int DocumentID { get; set; }
        public int AuthorID { get; set; }
        public virtual Author Author { get; set; }
        public virtual Document Document { get; set; }
    }
}
