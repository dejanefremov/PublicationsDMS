using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using PublicationsDMS.Entities.Interfaces.Services;
using PublicationsDMS.Entities.Models;
using PublicationsDMS.Web.Api.Models;
using PublicationsDMS.Web.Api.Attributes;

namespace PublicationsDMS.Web.Api.Folders
{
    public class FolderController : ApiController
    {
        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [UserAuthorize]
        public Folder Get(int folderID)
        {
            return _folderService.GetByID(folderID);
        }

        [AdminAuthorize]
        public void Post(Folder folder)
        {
            if (folder.ID == 0)
            {
                _folderService.AddFolder(folder);
            }
            else
            {
                _folderService.UpdateFolder(folder);
            }
        }
    }
}
