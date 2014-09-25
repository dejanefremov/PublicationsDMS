using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Models;
using PublicationsDMS.Entities.Models;
using PublicationsDMS.Web.Api.Models;

namespace PublicationsDMS.Web.Api.Registration.AutoMapperConverters
{
    public class NodeConverter : ITypeConverter<INode, Node>
    {
        public Node Convert(ResolutionContext context)
        {
            var sourceNode = context.SourceValue as INode;
            Node resultNode = null;

            if (sourceNode != null)
            {
                resultNode = new Node();

                resultNode.ID = sourceNode.ID;
                resultNode.ParentFolderID = sourceNode.ParentFolderID;
                resultNode.Title = sourceNode.Title;

                resultNode.TypeName = sourceNode.Type.ToString();
            }

            return resultNode;
        }
    }

    public class DataItemConverter : ITypeConverter<DataItem, Node>
    {
        public Node Convert(ResolutionContext context)
        {
            DataItem sourceNode = context.SourceValue as DataItem;
            Node resultNode = null;

            if (sourceNode != null)
            {
                resultNode = new Node();

                resultNode.ID = sourceNode.ID;
                resultNode.ParentFolderID = sourceNode.ParentFolderID;
                resultNode.Title = sourceNode.Title;

                resultNode.TypeName = sourceNode.Type.ToString();
            }

            return resultNode;
        }
    }

    public class DocumentConverter : ITypeConverter<Document, Node>
    {
        public Node Convert(ResolutionContext context)
        {
            var sourceNode = context.SourceValue as Document;
            Node resultNode = null;

            if (sourceNode != null)
            {
                resultNode = new Node();

                resultNode.ID = sourceNode.ID;
                resultNode.ParentFolderID = sourceNode.ParentFolderID;
                resultNode.Title = sourceNode.Title;

                resultNode.TypeName = sourceNode.Type.ToString();
            }

            return resultNode;
        }
    }

    public class FolderConverter : ITypeConverter<Folder, Node>
    {
        public Node Convert(ResolutionContext context)
        {
            var sourceNode = context.SourceValue as Folder;
            Node resultNode = null;

            if (sourceNode != null)
            {
                resultNode = new Node();

                resultNode.ID = sourceNode.ID;
                resultNode.ParentFolderID = sourceNode.ParentFolderID;
                resultNode.Title = sourceNode.Title;

                resultNode.TypeName = sourceNode.Type.ToString();
            }

            return resultNode;
        }
    }
}