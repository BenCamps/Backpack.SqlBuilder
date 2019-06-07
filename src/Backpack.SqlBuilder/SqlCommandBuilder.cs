using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class SqlCommandBuilder : SqlBuilder
    {

        protected SqlCommandBuilder() :base() { }

        protected SqlCommandBuilder(ISqlDialect dialect) : base(dialect)
        {
        }

        public void AppendTo(StringBuilder sb)
        {
            AppendTo(sb, Dialect);
        }

        public abstract void AppendTo(StringBuilder sb, ISqlDialect dialect);

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb);
            return sb.ToString();
        }
    }
}