using System;
using System.Collections.Generic;
using System.Text;
using Market.Abstract;
using Market.Abstract.Logic;
using Market.BLL;
using Market.Entities;
using Market.Models.DTO;
using System.Linq;
using AutoMapper;

namespace Market.BLL.Logic
{
    public class AccountManager : BaseLogic<UserDTO, User>, IAccountManager
    {
        public AccountManager(IDALContext ctx) : base(ctx)
        {
        }

        public void CreateDefaultAdmin()
        {
            if (!db.Users.Any(x => x.Login == "admin"))
            {
                db.Users.Add(new User { Login = "admin", Password = "admin" });
                base.Save();
            }
        }

        public UserDTO FindByLoginPassword(string login, string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Login == login && x.Password == password);

            var config = new MapperConfiguration(cfg=>cfg.CreateMap<User,UserDTO>());
            var mapper = config.CreateMapper();

            return mapper.Map<UserDTO>(user);
        }

        public bool Register(UserDTO model)
        {
            if(!db.Users.Any(x=>x.Login == model.Login))
            {
                base.Create(model);
                base.Save();
                return true;
            }
            return false;
        }
    }
}
