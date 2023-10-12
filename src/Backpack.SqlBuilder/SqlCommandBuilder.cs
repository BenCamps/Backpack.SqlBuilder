using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class SqlCommandBuilder : SqlBuilder, IAppendableElemant, ICompleatedSqlStatment
    {
        protected SqlCommandBuilder(ISqlDialect dialect) : base(dialect)
        {
        }

        protected abstract void AppendTo(StringBuilder sb, ISqlDialect dialect);

        void IAppendableElemant.AppendTo(StringBuilder sb, ISqlDialect dialect) => AppendTo(sb, dialect);

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb, Dialect);
            return sb.ToString();
        }

        string ICompleatedSqlStatment.ToSql()
        {
            return ToString();
        }
    }
}