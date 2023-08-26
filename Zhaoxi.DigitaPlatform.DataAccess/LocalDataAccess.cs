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

        public List<DevicesEntity> GetDevices()
        {
            var list = _client.GetInstance.Queryable<DevicesEntity>().ToList();

            return list;
        }

        public int SaveDevice(List<DevicesEntity> devices)
        {
            try
            {
                _client.GetInstance.BeginTran();

                // 先清空所有的组件，再从新保持

                _client.GetInstance.Deleteable<DevicesEntity>().ExecuteCommand();

                int ret = 0;

                if (devices.Count > 0)
                {
                    ret = _client.GetInstance.Insertable(devices).ExecuteCommand();
                }

                _client.GetInstance.CommitTran();

                return ret;
            }
            catch (Exception ex)
            {
                _client.GetInstance.RollbackTran();

                throw ex;
            }
        }
    }
}
