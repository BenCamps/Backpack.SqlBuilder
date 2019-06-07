using System;

namespace Backpack.SqlBuilder
{
    public abstract class SqlBuilder
    {
        public static ISqlDialect DefaultDialect { get; set; }

        ISqlDialect _dialect;

        protected SqlBuilder() { }

        protected SqlBuilder(ISqlDialect dialect)
        {
            Dialect = dialect ?? throw new ArgumentNullException(nameof(dialect));
        }

        public ISqlDialect Dialect
        {
            get => _dialect ?? DefaultDialect;
            set => _dialect = value;
        }

        public static SqlSelectBuilder Select(ISqlDialect dialect = null) => new SqlSelectBuilder(dialect ?? DefaultDialect);

        public static SqlInsertCommand Insert(ISqlDialect dialect = null) => new SqlInsertCommand(dialect ?? DefaultDialect);

        public static SqlUpdateCommand Update(ISqlDialect dialect = null) => new SqlUpdateCommand(dialect ?? DefaultDialect);

        public static CreateTable CreateTable(ISqlDialect dialect = null) => new CreateTable(dialect ?? DefaultDialect);


    }
}