using System.Text;

namespace Backpack.SqlBuilder
{
    public class LimitClause : SelectClause
    {
        public int LimitSize { get; set; }
        public int Offset { get; set; }

        public LimitClause(int limitSize, int offset)
        {
            LimitSize = limitSize;
            Offset = offset;
        }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            sb.Append("LIMIT " + LimitSize.ToString());
            if (Offset > 0)
            {
                sb.Append(" OFFSET " + Offset.ToString());
            }
        }
    }
}