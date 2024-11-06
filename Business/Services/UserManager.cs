using AutoMapper;
using Core.CustomExceptions;
using Core.Entities.Login;
using Core.Hash;
using DataAccsess.Concrete.SQL_EntityFrameWork;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class UserManager : ManagerBase<User, UserDto>, IUserService
    {
        public UserManager(IUserDal dal, IMapper mapper) : base(dal, mapper)
        {
        }

        public async Task<UserDto> UserVerify(UserLoginDto user)
        {
            try
            {
                string pass = HashHelper.HashItem(user.Password).ToString();
                var res = await this.GetListAsync(x => x.UserName == user.UserName && x.Password == pass);
                if (res.Count == 1)
                {
                    return res.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
    public interface IUserService : IServiceBase<User, UserDto>
    {
        Task<UserDto> UserVerify(UserLoginDto user);
    }
}
