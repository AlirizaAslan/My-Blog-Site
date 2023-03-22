using Blog.Core.Entities;
using Blog.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.UnitOfWorks
{
    public interface IUnitOfWork :IAsyncDisposable
    { //genel hal getirdik T ile erişim
        IRepository<T> GetRepository<T>()where T : class,IEntityBase,new();


        Task<int> SaveAsync();
        int Save();


    }
}
