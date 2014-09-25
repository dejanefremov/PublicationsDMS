using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class Document
    {
        public Document()
        {
            this.DocumentAuthors = new List<DocumentAuthor>();
            this.DocumentTags = new List<DocumentTag>();
        }

        public int DocumentID { get; set; }
        public System.Guid FileID { get; set; }
        public string Description { get; set; }
        public string FileExtension { get; set; }
        public virtual DataItem DataItem { get; set; }
        public virtual ICollection<DocumentAuthor> DocumentAuthors { get; set; }
        public virtual ICollection<DocumentTag> DocumentTags { get; set; }
    }
}
