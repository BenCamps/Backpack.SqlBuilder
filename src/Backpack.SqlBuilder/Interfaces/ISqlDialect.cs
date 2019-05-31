using System.Data;

namespace Backpack.SqlBuilder
{
    public interface ISqlDialect
    {
        string GetColumnDefinition(ColumnInfo columnInfo);

        string MapDbTypeToSQLType(DbType type);

        DbType MapSQLtypeToDbType(string sqlType);
    }
}