﻿using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Concrete
{
    public class DashboardService: IDashboardService
    {
        private readonly IUnitOfWork unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<List<int>> GetYearlyArticleCount()
        {
            var articles = await unitOfWork.GetRepository<Article>().GetAllAsycn(x => !x.IsDeleted);

            var startDate = DateTime.Now.Date;

            startDate=new DateTime(startDate.Year,1,1);  //mevuct olduğumuz yılın 1. ayın 1. gününden günümüze kadar

            List<int> datas = new();

            for(int i=1; i<=12; i++)
            {
                var startedDate=new DateTime(startDate.Year ,i,1);
                var endedDate=startedDate.AddMonths(1);
                var data = articles.Where(x => x.CreatedDate >= startedDate && x.CreatedDate < endedDate).Count();
                datas.Add(data);
            }

            return datas;
        }


    }
}
