using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class CommandElement
    {
        public abstract void AppendTo(StringBuilder sb, ISqlDialect dialect);

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb, SqlBuilder.DefaultDialect);
            return sb.ToString();
        }
    }
}