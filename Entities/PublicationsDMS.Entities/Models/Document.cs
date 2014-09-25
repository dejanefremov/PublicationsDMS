using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicationsDMS.Entities.Interfaces.Models;

namespace PublicationsDMS.Entities.Models
{
    public class Document : DataItem
    {
        public string FileExtension
        {
            get;
            set;
        }

        public Guid FileID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public List<Tag> Tags
        {
            get;
            set;
        }

        public List<Author> Authors
        {
            get;
            set;
        }
    }
}
