using System.Collections.Generic;
using CoolBaby.Application.ViewModels.Product;

namespace CoolBaby.Application.Interfaces
{
    public interface IPerfumeService
    {
        PerfumeViewModel Add(PerfumeViewModel product);

        void Update(PerfumeViewModel product);

        void Delete(int id);

        List<PerfumeViewModel> GetAll();

        PerfumeViewModel GetById(int id);

        void Save();
    }
}