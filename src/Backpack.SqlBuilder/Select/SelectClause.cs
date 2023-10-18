using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class SelectClause : IAppendableElemant
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb, SqlBuilder.DefaultDialect);
            return sb.ToString();
        }

        protected abstract void AppendTo(StringBuilder sb, ISqlDialect dialect);

        void IAppendableElemant.AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            AppendTo(sb, dialect);
        }
    }
}