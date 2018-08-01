using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class SqlBuilder
    {
        public abstract void AppendTo(StringBuilder sb);

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb);
            return sb.ToString();
        }
    }
}