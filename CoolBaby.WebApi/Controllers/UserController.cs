using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.System;
using CoolBaby.Utilities.Constants;
using CoolBaby.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBaby.WebApi.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    [Authorize]
    public class UserController : ApiController
    {
        #region Fields

        private readonly IAuthorizationService _authorizationService;
        private readonly IUserService _userService;

        #endregion Fields

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userService">User ser interface</param>
        /// <param name="authorizationService">Authorization service interface</param>
        public UserController(IUserService userService,
             IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _userService = userService;
        }

        #endregion Ctor

        #region Methods

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Users</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result =
                 await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.User, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();

            return new OkObjectResult(_userService.GetAllAsync());
        }

        /// <summary>
        /// Get by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result =
                await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.User, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();

            return new OkObjectResult(_userService.GetById(id));
        }

        /// <summary>
        /// Get all users paging
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <param name="page">page</param>
        /// <param name="pageSize">pageSize</param>
        /// <returns>Users</returns>
        [HttpGet]
        [Route("GetAllPaging")]
        public async Task<IActionResult> Get(string keyword, int page, int pageSize)
        {
            var result =
                await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.User, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();
            return new OkObjectResult(_userService.GetAllPagingAsync(keyword, page, pageSize));
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="appRoleViewModel">User</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppUserViewModel appRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            await _userService.AddAsync(appRoleViewModel);
            return new NoContentResult();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="appRoleViewModel">User</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AppUserViewModel appRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            await _userService.UpdateAsync(appRoleViewModel);
            return new NoContentResult();
        }

        /// <summary>
        ///Delete
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            await _userService.DeleteAsync(id);
            return new NoContentResult();
        }

        #endregion Methods
    }
}