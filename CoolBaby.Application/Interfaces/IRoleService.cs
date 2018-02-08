using CoolBaby.Application.ViewModels.System;
using CoolBaby.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolBaby.Application.Interfaces
{
    /// <summary>
    /// Role service interface
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Add a role
        /// </summary>
        /// <param name="appRoleViewModel">Role</param>
        /// <returns>Role</returns>
        Task<bool> AddAsync(AppRoleViewModel appRoleViewModel);

        /// <summary>
        /// Delete a role
        /// </summary>
        /// <param name="id">Role identifier</param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>Roles</returns>
        Task<List<AppRoleViewModel>> GetAllAsync();

        Task<List<AppRoleViewModel>> GetAllAsyncWithoutAdmin();

        /// <summary>
        /// Get all roles paging
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Roles</returns>
        PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int pageIndex, int pageSize);

        /// <summary>
        /// Get role by identifier
        /// </summary>
        /// <param name="id">Role identifier</param>
        /// <returns>Role</returns>
        Task<AppRoleViewModel> GetById(Guid id);

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="roleViewModel">Role</param>
        /// <returns></returns>
        Task UpdateAsync(AppRoleViewModel roleViewModel);

        /// <summary>
        /// Get list function with role
        /// </summary>
        /// <param name="roleId">Role identifier</param>
        /// <returns>Permissions</returns>
        List<PermissionViewModel> GetListFunctionWithRole(Guid roleId);

        /// <summary>
        /// Save Permission
        /// </summary>
        /// <param name="permissions">Permission</param>
        /// <param name="roleId">Role identifier</param>
        void SavePermission(List<PermissionViewModel> permissions, Guid roleId);

        /// <summary>
        /// Check Permission
        /// </summary>
        /// <param name="functionId">Function identifier</param>
        /// <param name="action">Action</param>
        /// <param name="roles">Roles</param>
        /// <returns>True/False</returns>
        Task<bool> CheckPermission(string functionId, string action, string[] roles);
    }
}