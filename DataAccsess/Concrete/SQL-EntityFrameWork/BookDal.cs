using Core.DataAccess;
using DataAccess.Contexts;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccsess.Concrete.SQL_EntityFrameWork
{
    public class BookDal : EfRepositoryBase<Book, EfContext>, IBookDal
    {
        public BookDal(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
    }

    public interface IBookDal : IEntityRepository<Book> { }
}
