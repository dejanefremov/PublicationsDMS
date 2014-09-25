using PublicationsDMS.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicationsDMS.Entities.Interfaces.Models
{
    public interface INode
    {
        int ID
        {
            get;
            set;
        }

        int? ParentFolderID
        {
            get;
            set;

        }

        string Title
        {
            get;
            set;
        }

        DataItemType Type
        {
            get;
            set;
        }
    }
}
