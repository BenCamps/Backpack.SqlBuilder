using System.Text;

namespace Backpack.SqlBuilder
{
    public class WhereClause : SelectClause
    {
        public static WhereClause operator +(WhereClause left, WhereClause right)
        {
            return new WhereClause(left.Expression + " AND " + right.Expression);
        }

        public WhereClause()
        { }

        public WhereClause(string expression)
        {
            Expression = expression;
        }

        public string Expression { get; set; }

        protected override void AppendTo(StringBuilder sb, ISqlDialect sqlDialect)
        {
            if (!string.IsNullOrEmpty(Expression))
            {
                sb.Append("WHERE ").Append(Expression);
            }
        }
    }
}