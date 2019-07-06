using System;
using System.Collections.Generic;
using System.Text;
using Market.Entities;
using Market.Models.DTO;

namespace Market.Abstract.Logic
{
    public interface IAccountManager : IBaseLogic<UserDTO, User>
    {
        void CreateDefaultAdmin();

        UserDTO FindByLoginPassword(string login, string password);

        bool Register(UserDTO model);
    }
}
