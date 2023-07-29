using SQLite;

namespace Repositories.Models
{
    public class Column
    {
        public string Name { get; set; }
        public string Type { get; set; }
        [Column("notnull")]
        public int NotNull { get; set; }
        [Column("dflt_value")]
        public string DefaultValue { get; set; }
        [Column("pk")]
        public string PrimaryKey { get; set; }
    }
}
