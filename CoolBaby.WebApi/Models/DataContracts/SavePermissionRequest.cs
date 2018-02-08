using CoolBaby.Application.ViewModels.System;
using System.Collections.Generic;

namespace CoolBaby.WebApi.Models.DataContracts
{
    public class SavePermissionRequest
    {
        public string FunctionId { set; get; }

        public IList<PermissionViewModel> Permissions { get; set; }
    }
}