using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backpack.SqlBuilder
{
    public class OrderByClause : SelectClause
    {
        public static OrderByClause operator +(OrderByClause left, OrderByClause right)
        {
            var exprs = left.OrderingTerms.Concat(right.OrderingTerms).ToArray();
            return new OrderByClause(exprs);
        }

        public IEnumerable<string> OrderingTerms { get; protected set; }

        private OrderByClause()
        {
            this.OrderingTerms = new List<string>();
        }

        public OrderByClause(IEnumerable<string> orderingTerms)
            : this()
        {
            OrderingTerms = orderingTerms;
        }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (OrderingTerms != null && OrderingTerms.Any())
            {
                sb.Append(" ORDER BY ");
                bool first = true;
                foreach (string term in OrderingTerms)
                {
                    if (!first)
                    {
                        sb.AppendLine(", ");
                    }
                    else
                    {
                        first = false;
                    }
                    sb.Append(term);
                }
            }
        }
    }
}