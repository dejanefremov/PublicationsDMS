using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class Tag
    {
        public Tag()
        {
            this.DocumentTags = new List<DocumentTag>();
        }

        public int TagID { get; set; }
        public string Text { get; set; }
        public virtual ICollection<DocumentTag> DocumentTags { get; set; }
    }
}
