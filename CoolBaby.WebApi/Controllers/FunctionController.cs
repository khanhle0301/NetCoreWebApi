using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.System;
using CoolBaby.Data.Entities;
using CoolBaby.Utilities.Constants;
using CoolBaby.WebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoolBaby.WebApi.Controllers
{
    [Authorize]
    public class FunctionController : ApiController
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IFunctionService _functionService;
        private readonly UserManager<AppUser> _userManager;

        public FunctionController(IFunctionService functionService,
            IAuthorizationService authorizationService,
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _authorizationService = authorizationService;
            _functionService = functionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string filter)
        {
            var result =
               await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.Function, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();
            return new OkObjectResult(_functionService.GetAll(filter));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result =
                await _authorizationService.AuthorizeAsync(User, CommonConstants.Functions.Role, Operations.Read);
            if (!result.Succeeded)
                return new ForbidResult();

            return new OkObjectResult(_functionService.GetById(id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] FunctionViewModel functionViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _functionService.Add(functionViewModel);
            _functionService.Save();
            return new NoContentResult();
        }

        [HttpPut]
        public IActionResult Update([FromBody] FunctionViewModel functionViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(string.Join(";", allErrors.Select(x => x.ErrorMessage)));
            }
            _functionService.Update(functionViewModel);
            _functionService.Save();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            _functionService.Delete(id);
            _functionService.Save();
            return new NoContentResult();
        }

        [Route("getlisthierarchy")]
        [HttpGet]
        public async Task<IActionResult> GetAllHierachyAsync()
        {
            var roles = ((ClaimsIdentity)User.Identity)
                .Claims.FirstOrDefault(x => x.Type == CommonConstants.UserClaims.Roles);
            bool IsAdmin = false;
            if (roles != null)
            {
                var listRole = JsonConvert.DeserializeObject<List<string>>(roles.Value).ToArray();
                if (listRole.Contains(CommonConstants.AppRole.AdminRole))
                {
                    IsAdmin = true;
                }
            }
            IEnumerable<FunctionViewModel> model;
            if (IsAdmin)
            {
                var functions = _functionService.GetAll(string.Empty);
                model = functions.Result;
            }
            else
            {
                var userId = _userManager.GetUserId(User);
                model = _functionService.GetAllWithPermission(userId);
            }

            var parents = model.Where(x => x.ParentId == null);
            foreach (var parent in parents)
            {
                parent.ChildFunctions = model.Where(x => x.ParentId == parent.Id).ToList();
            }
            return new OkObjectResult(parents);
        }
    }
}