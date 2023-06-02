using SqlSugar;
using System.Linq;

namespace Zhaoxi.DigitaPlatform.DataAccess
{
    public class DBClient
    {
        private static readonly string _connectionString = "Data Source=data.jovan";//文件路径

        private readonly SqlSugarScope _client;

        public DBClient()
        {
            var connectionConfig = new ConnectionConfig
            {
                DbType = DbType.Sqlite,
                ConnectionString = _connectionString,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true
            };
#if DEBUG
            connectionConfig.AopEvents = new AopEvents
            {
                OnLogExecuting = (sql, p) =>
                {
                    System.Console.WriteLine(sql);
                    System.Console.WriteLine(string.Join(",", p?.Select(it => it.ParameterName + ":" + it.Value)));
                }
            };
#endif
            _client = new SqlSugarScope(connectionConfig);
        }

        /// <summary>
        /// 获取SqlSugarScope实例
        /// </summary>
        public SqlSugarScope GetInstance => _client;
    }
}
