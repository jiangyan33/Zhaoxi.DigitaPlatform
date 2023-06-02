using SqlSugar;

namespace Zhaoxi.DigitaPlatform.DataAccess.Entities
{

    [SugarTable("sys_users")]
    public class SysUsersEntity
    {

        [SugarColumn(IsPrimaryKey = true, ColumnName = "id", IsIdentity = true)]

        public int Id { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }


        [SugarColumn(ColumnName = "password")]
        public string Password { get; set; }


        [SugarColumn(ColumnName = "user_type")]
        public string UserType { get; set; }


        [SugarColumn(ColumnName = "real_name")]
        public string RealName { get; set; }


        [SugarColumn(ColumnName = "gender")]
        public int Gender { get; set; }


        [SugarColumn(ColumnName = "phone_num")]
        public string PhoneNum { get; set; }

        [SugarColumn(ColumnName = "department")]
        public string Department { get; set; }

    }
}
