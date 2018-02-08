using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.System;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoolBaby.Application.Implementation
{
    public class PermissionService : IPermissionService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        private IFunctionRepository _functionRepository;
        private IPermissionRepository _permissionRepository;
        private IUnitOfWork _unitOfWork;

        public PermissionService(IPermissionRepository permissionRepository,
            IFunctionRepository functionRepository,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            IConfiguration configuration,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _functionRepository = functionRepository;
            this._permissionRepository = permissionRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(PermissionViewModel permissionVm)
        {
            var permission = Mapper.Map<PermissionViewModel, Permission>(permissionVm);
            _permissionRepository.Add(permission);
        }

        public void DeleteAll(string functionId)
        {
            _permissionRepository.DeleteMulti(x => x.FunctionId == functionId);
        }

        public List<PermissionViewModel> GetByFunctionId(string functionId)
        {
            return _permissionRepository
                .FindAll(x => x.FunctionId == functionId)
                .ProjectTo<PermissionViewModel>().ToList();
        }

        public async Task<IEnumerable<PermissionViewModel>> GetByUserId(string userId)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userId", userId);

                try
                {
                    return await sqlConnection.QueryAsync<PermissionViewModel>(
                        "GetByUserId", dynamicParameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }
    }
}