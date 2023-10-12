using System;
using System.Data;
using System.Text;

namespace Backpack.SqlBuilder.Sqlite
{
    public class SqliteDialect : SqlDialect
    {
        public override string GetColumnDefinition(ColumnInfo col)
        {
            var sqlType = col.Type ?? MapDbTypeToSQLType(col.DBType);
            if (col.AutoIncrement && sqlType != SqliteDataType.INTEGER) { throw new InvalidOperationException("column type must be integer type if AutoIncrement is true"); }

            StringBuilder columnDef = new StringBuilder(col.Name).Append(" ").Append(sqlType);
            if (col.IsPK)
            {
                columnDef.Append(" PRIMARY KEY");
                if (col.AutoIncrement)
                { columnDef.Append(" AUTOINCREMENT"); }
            }

            if (col.NotNull)
            {
                columnDef.Append(" NOT NULL");
            }
            else if (!string.IsNullOrEmpty(col.Default))
            {
                columnDef.Append(" DEFAULT ").Append(col.Default);
            }

            if (col.Unique)
            {
                columnDef.Append(" UNIQUE");
            }

            if (!string.IsNullOrEmpty(col.Check))
            {
                columnDef.Append(" CHECK ").Append(col.Check);
            }

            return columnDef.ToString();
        }

        public override string MapDbTypeToSQLType(DbType type)
        {
            switch (type)
            {
                case DbType.AnsiString:
                case DbType.AnsiStringFixedLength:
                case DbType.Guid:
                case DbType.String:
                case DbType.StringFixedLength:
                case DbType.Xml:
                    { return SqliteDataType.TEXT; }
                case DbType.Int16:
                case DbType.Int32:
                case DbType.Int64:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                    { return SqliteDataType.INTEGER; }
                case DbType.Binary:
                    { return SqliteDataType.BLOB; }
                case DbType.Double:
                case DbType.Single:
                    { return SqliteDataType.REAL; }
                case DbType.Date:
                case DbType.DateTime:
                case DbType.DateTime2:
                case DbType.DateTimeOffset:
                case DbType.Time:
                case DbType.Byte:
                case DbType.Boolean:
                case DbType.Decimal:
                case DbType.VarNumeric:
                    { return SqliteDataType.NUMERIC; }
                default:
                    { return SqliteDataType.TEXT; }
            }
        }

        public override DbType MapSQLtypeToDbType(string sqlType)
        {
            if (sqlType == null) { throw new ArgumentNullException("sqlType"); }
            sqlType = sqlType.ToUpper(System.Globalization.CultureInfo.InvariantCulture);
            switch (sqlType)
            {
                case SqliteDataType.BLOB:
                    { return DbType.Binary; }
                case SqliteDataType.INTEGER:
                    { return DbType.Int64; }
                case SqliteDataType.REAL:
                    { return DbType.Double; }
                case SqliteDataType.TEXT:
                    { return DbType.String; }
                case "BOOL":
                case SqliteDataType.BOOLEAN:
                    { return DbType.Boolean; }
                case SqliteDataType.DATETIME:
                    { return DbType.DateTime; }
                case SqliteDataType.DOUBLE:
                    { return DbType.Double; }
                case SqliteDataType.FLOAT:
                    { return DbType.Single; }
                default:
                    { throw new ArgumentException("value:" + sqlType + " is invalid", "sqlType"); }
            }
        }

        public override Type MapSQLtypeToSystemType(string sqlType)
        {
            if (sqlType == null) { throw new ArgumentNullException("sqlType"); }
            sqlType = sqlType.ToUpper(System.Globalization.CultureInfo.InvariantCulture);
            switch (sqlType)
            {
                case SqliteDataType.BLOB:
                    { return typeof(byte[]); }
                case SqliteDataType.INTEGER:
                    { return typeof(int); }
                case SqliteDataType.FLOAT:
                    { return typeof(float); }
                case SqliteDataType.DOUBLE:
                case SqliteDataType.REAL:
                    { return typeof(double); }
                case SqliteDataType.TEXT:
                    { return typeof(string); }
                case "BOOL":
                case SqliteDataType.BOOLEAN:
                    { return typeof(bool); }
                case SqliteDataType.DATETIME:
                    { return typeof(DateTime); }
                default:
                    { throw new ArgumentException("value:" + sqlType + " is invalid", "sqlType"); }
            }
        }

        public override string MapSystemTypeToSQLtype(Type type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }
            type = Nullable.GetUnderlyingType(type) ?? type;

            var typeCode = Type.GetTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.Boolean:
                    { return SqliteDataType.BOOLEAN; }
                case TypeCode.Char:
                case TypeCode.String:
                    { return SqliteDataType.TEXT; }
                case TypeCode.DateTime:
                    { return SqliteDataType.DATETIME; }
                case TypeCode.Decimal:
                case TypeCode.Double:
                    { return SqliteDataType.DOUBLE; }
                case TypeCode.Single:
                    { return SqliteDataType.FLOAT; }
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    { return SqliteDataType.INTEGER; }
                default:
                    { throw new ArgumentException("value:" + type + " is invalid", "type"); }
            }
        }
    }
}