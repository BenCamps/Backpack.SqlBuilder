using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class SelectClause
    {
        public abstract void AppendTo(StringBuilder sb);
    }
}