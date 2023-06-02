using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zhaoxi.DigitaPlatform.DataAccess.Entities;

namespace Zhaoxi.DigitaPlatform.DataAccess
{
    public class LocalDataAccess
    {
        private readonly DBClient _client;

        public LocalDataAccess(DBClient client)
        {
            _client = client;
        }

        public SysUsersEntity Login(string username, string password)
        {

            var list = _client.GetInstance.Queryable<SysUsersEntity>().Where(x => x.UserName == username && x.Password == password).ToList();

            if (list.Count == 0)
            {
                throw new Exception("用户名或者密码错误");
            }
            return list.First();
        }
    }
}
