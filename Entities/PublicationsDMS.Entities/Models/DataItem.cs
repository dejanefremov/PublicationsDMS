using PublicationsDMS.Entities.Enumerations;
using PublicationsDMS.Entities.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Entities.Models
{
    public class DataItem : INode
    {
        public int ID
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public int? ParentFolderID
        {
            get;
            set;
        }


        public DataItemType Type
        {
            get;
            set;
        }
    }
}
