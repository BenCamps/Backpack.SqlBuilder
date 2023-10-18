using System.Text;

namespace Backpack.SqlBuilder
{
    public class JoinClause : SelectClause
    {
        public string JoinType { get; set; }
        public TableOrSubQuery Source { get; set; }
        public string JoinConstraint { get; set; }

        public JoinClause(TableOrSubQuery source, string constraint)
        {
            Source = source;
            JoinConstraint = constraint;
        }

        public JoinClause(string table, string constraint)
            : this(new TableOrSubQuery(table, null), constraint)
        {
        }

        public JoinClause(string table, string constraint, string alias)
            : this(new TableOrSubQuery(table, alias), constraint)
        {
        }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (JoinType != null) { sb.Append(JoinType).Append(" "); }
            sb.Append("JOIN ").Append(Source).Append(" ").Append(JoinConstraint);
        }
    }
}