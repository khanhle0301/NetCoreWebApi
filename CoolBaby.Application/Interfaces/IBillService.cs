﻿using System;
using System.Collections.Generic;
using System.Text;
using CoolBaby.Application.ViewModels.Product;
using CoolBaby.Data.Enums;
using CoolBaby.Utilities.Dtos;

namespace CoolBaby.Application.Interfaces
{
    public interface IBillService
    {
        void Create(BillViewModel billVm);
        void Update(BillViewModel billVm);

        PagedResult<BillViewModel> GetAllPaging(string startDate, string endDate, string keyword,
            int pageIndex, int pageSize);

        BillViewModel GetDetail(int billId);

        BillDetailViewModel CreateDetail(BillDetailViewModel billDetailVm);

        void DeleteDetail(int productId, int billId, int colorId, int sizeId);

        void UpdateStatus(int orderId, BillStatus status);

        List<BillDetailViewModel> GetBillDetails(int billId);

        List<ColorViewModel> GetColors();

        List<PerfumeViewModel> GetPerfumes();

        List<SizeViewModel> GetSizes();

        List<ColorViewModel> GetColors(int productId);

        void Save();
    }
}
