using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Advertistment;

namespace CoolBaby.Application.Interfaces
{
    public interface IAdvertistmentService
    {
        AdvertistmentViewModel Add(AdvertistmentViewModel advertistment);

        void Update(AdvertistmentViewModel advertistment);

        void Delete(int id);

        List<AdvertistmentViewModel> GetAll();

        List<AdvertistmentViewModel> GetByPositionId(string positionId);

        AdvertistmentViewModel GetById(int id);

        void Save();
    }
}