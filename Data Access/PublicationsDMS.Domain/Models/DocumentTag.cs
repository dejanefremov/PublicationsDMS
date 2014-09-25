using System;
using System.Collections.Generic;

namespace PublicationsDMS.Domain.Models
{
    public partial class DocumentTag
    {
        public int DocumentTagID { get; set; }
        public int DocumentID { get; set; }
        public int TagID { get; set; }
        public virtual Document Document { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
