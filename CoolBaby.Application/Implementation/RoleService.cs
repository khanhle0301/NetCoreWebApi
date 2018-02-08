using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.System;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;
using CoolBaby.Utilities.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBaby.Application.Implementation
{
    /// <summary>
    /// Role Service
    /// </summary>
    public class RoleService : IRoleService
    {
        #region Fields

        private RoleManager<AppRole> _roleManager;
        private IFunctionRepository _functionRepository;
        private IPermissionRepository _permissionRepository;
        private IUnitOfWork _unitOfWork;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="roleManager">Role manager</param>
        /// <param name="unitOfWork">Unit of work interface</param>
        /// <param name="functionRepository">Function repository</param>
        /// <param name="permissionRepository">Permission repository</param>
        public RoleService(RoleManager<AppRole> roleManager, IUnitOfWork unitOfWork,
         IFunctionRepository functionRepository, IPermissionRepository permissionRepository)
        {
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _functionRepository = functionRepository;
            _permissionRepository = permissionRepository;
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Add role
        /// </summary>
        /// <param name="appRoleViewModel">Role</param>
        /// <returns>Role</returns>
        public async Task<bool> AddAsync(AppRoleViewModel appRoleViewModel)
        {
            var role = new AppRole()
            {
                Name = appRoleViewModel.Name,
                Description = appRoleViewModel.Description
            };
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        /// <summary>
        /// Check permission
        /// </summary>
        /// <param name="functionId">Function identifier</param>
        /// <param name="action">Action</param>
        /// <param name="roles">Roles</param>
        /// <returns>True/False</returns>
        public Task<bool> CheckPermission(string functionId, string action, string[] roles)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in _roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Id == functionId
                        && ((p.CanCreate && action ==
                        Utilities.Constants.CommonConstants.Actions.Create)
                        || (p.CanUpdate && action ==
                        Utilities.Constants.CommonConstants.Actions.Update)
                        || (p.CanDelete && action ==
                        Utilities.Constants.CommonConstants.Actions.Delete)
                        || (p.CanRead && action ==
                        Utilities.Constants.CommonConstants.Actions.Read))
                        select p;
            return query.AnyAsync();
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="id">Role identifier</param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
        }

        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>Roles</returns>
        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles.ProjectTo<AppRoleViewModel>().ToListAsync();
        }

        public async Task<List<AppRoleViewModel>> GetAllAsyncWithoutAdmin()
        {
            return await _roleManager.Roles.Where(x => x.Name != "Admin")
                .ProjectTo<AppRoleViewModel>().ToListAsync();
        }

        /// <summary>
        /// Get all roles paging
        /// </summary>
        /// <param name="keyword">Keyword</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Roles</returns>
        public PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int pageIndex, int pageSize)
        {
            var query = _roleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword)
                || x.Description.Contains(keyword));

            int totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize)
               .Take(pageSize);

            var data = query.ProjectTo<AppRoleViewModel>().ToList();
            var paginationSet = new PagedResult<AppRoleViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        /// <summary>
        /// Get role by identifier
        /// </summary>
        /// <param name="id">Role identifier</param>
        /// <returns>Role</returns>
        public async Task<AppRoleViewModel> GetById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return Mapper.Map<AppRole, AppRoleViewModel>(role);
        }

        /// <summary>
        /// Get list function with role
        /// </summary>
        /// <param name="roleId">Role identifier</param>
        /// <returns></returns>
        public List<PermissionViewModel> GetListFunctionWithRole(Guid roleId)
        {
            var functions = _functionRepository.FindAll();
            var permissions = _permissionRepository.FindAll();

            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId into fp
                        from p in fp.DefaultIfEmpty()
                        where p != null && p.RoleId == roleId
                        select new PermissionViewModel()
                        {
                            RoleId = roleId,
                            FunctionId = f.Id,
                            CanCreate = p != null ? p.CanCreate : false,
                            CanDelete = p != null ? p.CanDelete : false,
                            CanRead = p != null ? p.CanRead : false,
                            CanUpdate = p != null ? p.CanUpdate : false
                        };

            return query.ToList();
        }

        /// <summary>
        /// Save permission
        /// </summary>
        /// <param name="permissionVms">Permission</param>
        /// <param name="roleId">Role identifier</param>
        public void SavePermission(List<PermissionViewModel> permissionVms, Guid roleId)
        {
            var permissions = Mapper.Map<List<PermissionViewModel>, List<Permission>>(permissionVms);
            var oldPermission = _permissionRepository.FindAll().Where(x => x.RoleId == roleId).ToList();
            if (oldPermission.Count > 0)
            {
                _permissionRepository.RemoveMultiple(oldPermission);
            }
            foreach (var permission in permissions)
            {
                _permissionRepository.Add(permission);
            }
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="appRoleViewModel">Role</param>
        /// <returns></returns>
        public async Task UpdateAsync(AppRoleViewModel appRoleViewModel)
        {
            var role = await _roleManager.FindByIdAsync(appRoleViewModel.Id.ToString());
            role.Description = appRoleViewModel.Description;
            role.Name = appRoleViewModel.Name;
            await _roleManager.UpdateAsync(role);
        }

        #endregion Methods
    }
}