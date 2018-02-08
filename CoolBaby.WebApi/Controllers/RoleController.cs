using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.System;
using CoolBaby.Utilities.Constants;
using CoolBaby.WebApi.Authorization;
using CoolBaby.WebApi.Models.DataContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBaby.WebApi.Controllers
{
    /// <summary>
    /// Role controller
    /// </summary>
    [Authorize]
    public class RoleController : ApiController
    {
        #region Fields

        private readonly IAuthorizationService _authorizationService;
        private readonly IPermissionService _permissionService;
        private readonly IRoleService _roleService;
        private readonly IFunctionService _functionService;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="roleService">Role service</param>
        /// <param name="authorizationService">Authorization service</param>
        /// <param name="permissionService">Permission Service Interface</param>
        /// <param name="functionService">Function Service Interface</param>
        public RoleController(IRoleService roleService,
            IAuthorizationService authorizationService,
            IPermissionService permissionService,
            IFunctionService functionService)
        {
            _functionService = functionService;
            _permissionService = permissionService;
            _authorizationService = authorizationService;
            _roleService = roleService;
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Get all Role
        /// </summary>
        /// <returns>List role</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result =
                await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.Role, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();

            return new OkObjectResult(_roleService.GetAllAsync());
        }

        /// <summary>
        /// Get by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result =
                await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.Role, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();

            return new OkObjectResult(_roleService.GetById(id));
        }

        /// <summary>
        /// Get all paging Role
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <param name="page">page</param>
        /// <param name="pageSize">pageSize</param>
        /// <returns>List role paging </returns>
        [HttpGet]
        [Route("GetAllPaging")]
        public async Task<IActionResult> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var result =
                await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.Role, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();

            return new OkObjectResult(_roleService.GetAllPagingAsync(keyword, page, pageSize));
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="appRoleViewModel">Role view model</param>
        /// <returns>No Content</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppRoleViewModel appRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(string.Join("\r\n", allErrors.Select(x => x.ErrorMessage)));
            }
            await _roleService.AddAsync(appRoleViewModel);
            return new NoContentResult();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="appRoleViewModel">Role view model</param>
        /// <returns>No Content</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AppRoleViewModel appRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            await _roleService.UpdateAsync(appRoleViewModel);
            return new NoContentResult();
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">Role identifier</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            await _roleService.DeleteAsync(id);
            return new NoContentResult();
        }

        [Route("getAllPermission")]
        [HttpGet]
        public IActionResult GetAllPermission(string functionId)
        {
            List<PermissionViewModel> permissions = new List<PermissionViewModel>();
            var roles = _roleService.GetAllAsyncWithoutAdmin().Result;

            var listPermission = _permissionService.GetByFunctionId(functionId);
            if (listPermission.Count == 0)
            {
                foreach (var item in roles)
                {
                    permissions.Add(new PermissionViewModel()
                    {
                        RoleId = item.Id,
                        CanCreate = false,
                        CanDelete = false,
                        CanRead = false,
                        CanUpdate = false,
                        AppRole = new AppRoleViewModel()
                        {
                            Id = item.Id,
                            Description = item.Description,
                            Name = item.Name
                        }
                    });
                }
            }
            else
            {
                foreach (var item in roles)
                {
                    if (!listPermission.Any(x => x.RoleId == item.Id))
                    {
                        permissions.Add(new PermissionViewModel()
                        {
                            RoleId = item.Id,
                            CanCreate = false,
                            CanDelete = false,
                            CanRead = false,
                            CanUpdate = false,
                            AppRole = new AppRoleViewModel()
                            {
                                Id = item.Id,
                                Description = item.Description,
                                Name = item.Name
                            }
                        });
                    }
                    permissions = listPermission;
                }
            }
            return new OkObjectResult(permissions);
        }

        [HttpPost]
        [Route("savePermission")]
        public async Task<IActionResult> SavePermission([FromBody] SavePermissionRequest data)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            try
            {
                _permissionService.DeleteAll(data.FunctionId);
                foreach (var item in data.Permissions)
                {
                    var permissionVm = new PermissionViewModel()
                    {
                        CanCreate = item.CanCreate,
                        CanDelete = item.CanDelete,
                        CanRead = item.CanRead,
                        CanUpdate = item.CanUpdate,
                        RoleId = item.RoleId,
                        FunctionId = data.FunctionId
                    };
                    _permissionService.Add(permissionVm);
                }
                var functions = _functionService.GetAllWithParentId(data.FunctionId);
                if (functions.Any())
                {
                    foreach (var item in functions)
                    {
                        _permissionService.DeleteAll(item.Id);

                        foreach (var p in data.Permissions)
                        {
                            var childPermissionVm = new PermissionViewModel();
                            childPermissionVm.FunctionId = item.Id;
                            childPermissionVm.RoleId = p.RoleId;
                            childPermissionVm.CanRead = p.CanRead;
                            childPermissionVm.CanCreate = p.CanCreate;
                            childPermissionVm.CanDelete = p.CanDelete;
                            childPermissionVm.CanUpdate = p.CanUpdate;
                            _permissionService.Add(childPermissionVm);
                        }
                    }
                }
                _permissionService.SaveChange();
                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        #endregion Methods
    }
}