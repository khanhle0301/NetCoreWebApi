using CoolBaby.Application.ViewModels.System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolBaby.Application.Interfaces
{
    public interface IPermissionService
    {
        List<PermissionViewModel> GetByFunctionId(string functionId);

        void Add(PermissionViewModel permission);

        void DeleteAll(string functionId);

        Task<IEnumerable<PermissionViewModel>> GetByUserId(string userId);

        void SaveChange();
    }
}