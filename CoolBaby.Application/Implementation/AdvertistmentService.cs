using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using CoolBaby.Application.Interfaces;
using CoolBaby.Application.ViewModels.Advertistment;
using CoolBaby.Data.Entities;
using CoolBaby.Data.IRepositories;
using CoolBaby.Infrastructure.Interfaces;

namespace CoolBaby.Application.Implementation
{
    public class AdvertistmentService : IAdvertistmentService
    {
        private readonly IAdvertistmentRepository _advertistmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdvertistmentService(IAdvertistmentRepository AdvertistmentRepository,
            IUnitOfWork unitOfWork)
        {
            _advertistmentRepository = AdvertistmentRepository;
            _unitOfWork = unitOfWork;
        }

        public AdvertistmentViewModel Add(AdvertistmentViewModel AdvertistmentVm)
        {
            var Advertistment = Mapper.Map<AdvertistmentViewModel, Advertistment>(AdvertistmentVm);
            _advertistmentRepository.Add(Advertistment);
            return AdvertistmentVm;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AdvertistmentViewModel> GetAll()
        {
            return _advertistmentRepository.FindAll()
               .ProjectTo<AdvertistmentViewModel>().ToList();
        }

        public AdvertistmentViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AdvertistmentViewModel> GetByPositionId(string positionId)
        {
            return _advertistmentRepository.FindAll(x => x.PositionId == positionId)
              .ProjectTo<AdvertistmentViewModel>().ToList();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(AdvertistmentViewModel product)
        {
            throw new NotImplementedException();
        }
    }
}