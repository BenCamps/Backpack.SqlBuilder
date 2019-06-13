using System;
using System.Data;

namespace Backpack.SqlBuilder
{
    public abstract class SqlDialect : ISqlDialect
    {
        public static ISqlDialect DefaultDialect { get; set; }

        public abstract string GetColumnDefinition(ColumnInfo columnInfo);

        public string ConvertToString(ForeignKeyTriggers fkTrigger)
        {
            switch (fkTrigger)
            {
                case ForeignKeyTriggers.Cascade: return "CASCADE ";
                case ForeignKeyTriggers.Restrict: return "RESTRICT ";
                case ForeignKeyTriggers.SetDefault: return "SET DEFAULT ";
                case ForeignKeyTriggers.SetNull: return "SET NULL ";
                default: return null;
            }
        }

        public string ConvertToString(Deferrable deferrable)
        {
            switch (deferrable)
            {
                case Deferrable.Deferrable: return "DEFERRABLE ";
                case Deferrable.InitiallyDeferred: return "INITIALLY DEFERRED ";
                case Deferrable.InitiallyImmediate: return "INITIALLY IMMEDIATE ";
                case Deferrable.NotDeferrable: return "NOT DEFERRABLE ";
                case Deferrable.Default: return null;
                default: return null;
            }
        }

        public abstract string MapDbTypeToSQLType(DbType type);

        public abstract DbType MapSQLtypeToDbType(string sqlType);

        public abstract Type MapSQLtypeToSystemType(string sqlType);

        public abstract string MapSystemTypeToSQLtype(Type type);
    }
}