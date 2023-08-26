using SqlSugar;

namespace Zhaoxi.DigitaPlatform.DataAccess.Entities
{
    /// <summary>
    /// 自定义组件存储信息
    /// </summary>
    [SugarTable("devices")]
    public class DevicesEntity
    {
        [SugarColumn(IsPrimaryKey = true, ColumnName = "id", IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "device_num")]
        public string DeviceNum { get; set; }

        [SugarColumn(ColumnName = "x")]
        public string X { get; set; }

        [SugarColumn(ColumnName = "y")]
        public string Y { get; set; }

        [SugarColumn(ColumnName = "z")]
        public string Z { get; set; }

        [SugarColumn(ColumnName = "w")]
        public string W { get; set; }

        [SugarColumn(ColumnName = "h")]
        public string H { get; set; }

        [SugarColumn(ColumnName = "device_type_name")]
        public string DeviceTypeName { get; set; }

        [SugarColumn(ColumnName = "header")]
        public string Header { get; set; }
    }
}
