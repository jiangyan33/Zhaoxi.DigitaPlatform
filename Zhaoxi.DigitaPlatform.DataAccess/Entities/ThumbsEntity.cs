using SqlSugar;

namespace Zhaoxi.DigitaPlatform.DataAccess.Entities
{
    [SugarTable("thumbs")]
    public class ThumbsEntity
    {
        [SugarColumn(IsPrimaryKey = true, ColumnName = "id", IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(ColumnName = "icon")]
        public string Icon { get; set; }

        [SugarColumn(ColumnName = "target_type")]
        public string TargetType { get; set; }

        [SugarColumn(ColumnName = "header")]
        public string Header { get; set; }

        [SugarColumn(ColumnName = "w")]
        public int W { get; set; }

        [SugarColumn(ColumnName = "h")]
        public int H { get; set; }

        [SugarColumn(ColumnName = "category")]
        public string Category { get; set; }
    }
}
