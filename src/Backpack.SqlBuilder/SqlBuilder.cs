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

        public static SqlSelectBuilder Select => new SqlSelectBuilder(DefaultDialect);

        public static SqlInsertCommand Insert => new SqlInsertCommand(DefaultDialect);

        public static CreateTable CreateTable => new CreateTable(DefaultDialect);
    }
}