namespace Backpack.SqlBuilder
{
    public abstract class SqlBuilder
    {
        public static ISqlDialect DefaultDialect { get; set; }

        protected SqlBuilder(ISqlDialect dialect)
        {
            Dialect = dialect ?? DefaultDialect;
        }

        public ISqlDialect Dialect { get; set; }

        public static SqlSelectBuilder Select(ISqlDialect dialect = null) => new SqlSelectBuilder(dialect ?? DefaultDialect);

        public static SqlInsertCommand InsertInto(string tableName, OnConflictOption conflictOption = OnConflictOption.Default, ISqlDialect dialect = null)
            => new SqlInsertCommand(tableName, conflictOption, dialect ?? DefaultDialect);

        public static SqlUpdateCommand Update(string tableName, OnConflictOption conflictOption, ISqlDialect dialect = null)
            => new SqlUpdateCommand(dialect ?? DefaultDialect);

        public static CreateTable CreateTable(string tableName, ISqlDialect dialect = null)
            => new CreateTable(tableName, dialect ?? DefaultDialect);
    }
}