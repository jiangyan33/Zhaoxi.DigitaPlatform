using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace Zhaoxi.DigitaPlatform.DataAccess
{
    public class LocalDataAccess
    {
        #region 基础方法

        private SQLiteConnection _conn = null;

        private SQLiteCommand _comm = null;

        private SQLiteDataAdapter _adapter = null;

        private SQLiteTransaction _trans = null;

        private readonly string connStr = "Data Source=data.jovan;Version=3;";

        private DataTable GetDatas(string sql, Dictionary<string, object> param = null)
        {
            try
            {
                if (_conn == null)
                {
                    _conn = new SQLiteConnection(connStr);
                }

                _conn.Open();

                _adapter = new SQLiteDataAdapter(sql, _conn);

                if (param != null)
                {
                    foreach (var item in param)
                    {
                        _adapter.SelectCommand.Parameters.Add(new SQLiteParameter(item.Key, DbType.String) { Value = item.Value });
                    }
                }

                var dataTable = new DataTable();

                _adapter.Fill(dataTable);

                return dataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }


        private int SqlExecute(List<string> sqls)
        {
            int count = 0;

            try
            {
                if (_conn == null)
                {
                    _conn = new SQLiteConnection(connStr);
                }

                _conn.Open();

                _trans = _conn.BeginTransaction();

                foreach (var item in sqls)
                {
                    _comm = new SQLiteCommand(item, _conn);

                    count += _comm.ExecuteNonQuery();
                }
                _trans.Commit();

                return count;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.Dispose();
            }
        }

        private void Dispose()
        {
            if (_trans != null)
            {
                _trans.Dispose();
                _trans = null;
            }
            if (_adapter != null)
            {
                _adapter.Dispose();
                _adapter = null;
            }
            if (_comm != null)
            {
                _comm.Dispose();
                _comm = null;
            }
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
                _conn = null;
            }
        }
        #endregion


        public DataTable Login(string username, string password)
        {
            var userSql = "select * from sys_users where user_name=@user_name and password=@password";

            var param = new Dictionary<string, object>();

            param.Add("@user_name", username);
            param.Add("@password", password);

            var dataTable = GetDatas(userSql, param);

            if (dataTable.Rows.Count == 0)
            {
                throw new Exception("用户名或者密码错误");
            }

            return dataTable;
        }

    }
}
