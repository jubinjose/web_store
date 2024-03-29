﻿using Store.Model;
using Store.Model.DTO;

namespace Store.Service
{
    public interface IAccountService
    {
        OpResult CreateAccount(AccountCreateRequest dto);

        AccountResponse GetAccount(string userName);

        OpResult UpdateAccount(string userName, AccountUpdateRequest dto);

        AccountResponse GetAccount(int id);

        OpResult UpdateAccount(AccountUpdateRequest dto);

        void DeleteAccount(int id);
    }
}
