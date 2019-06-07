using System.Data;

namespace Backpack.SqlBuilder
{
    public interface ISqlDialect
    {
        string ConvertToString(ForeignKeyTriggers fkTrigger);

        string ConvertToString(Deferrable deferrable);

        string GetColumnDefinition(ColumnInfo columnInfo);

        string MapDbTypeToSQLType(DbType type);

        DbType MapSQLtypeToDbType(string sqlType);
    }
}