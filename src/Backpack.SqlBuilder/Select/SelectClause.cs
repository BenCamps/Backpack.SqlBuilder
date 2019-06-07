using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class SelectClause
    {
        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb);
            return sb.ToString();
        }

        public abstract void AppendTo(StringBuilder sb);
    }
}