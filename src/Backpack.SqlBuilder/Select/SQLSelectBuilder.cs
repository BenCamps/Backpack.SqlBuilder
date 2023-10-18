using System;
using System.Collections.Generic;
using System.Text;

namespace Backpack.SqlBuilder
{
    public class SqlSelectBuilder : SqlCommandBuilder, IAcceptsJoin
    {
        public TableOrSubQuery Source { get; set; }
        public IList<string> ResultColumns { get; protected set; } = new ResultColumnCollection();

        public IList<JoinClause> JoinClauses { get; protected set; } = new List<JoinClause>();

        public WhereClause WhereClause { get; set; }
        public GroupByClause GroupByClause { get; set; }
        public OrderByClause OrderByClause { get; set; }
        public LimitClause LimitClause { get; set; }

        public IList<WhereClause> Clauses { get; set; }

        public SqlSelectBuilder(ISqlDialect dialect = null) : base(dialect)
        {
        }

        public IAcceptsJoin From(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) { throw new ArgumentException($"'{nameof(tableName)}' cannot be null or whitespace.", nameof(tableName)); }

            if (Source != null) throw new InvalidOperationException("Select Source already Set");

            Source = new TableOrSubQuery(tableName);
            return this;
        }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (ResultColumns == null) { throw new ArgumentNullException("ResultColumns"); }

            sb.Append("SELECT ");
            sb.Append(ResultColumns.ToString());

            if (Source != null)
            {
                sb.Append(" FROM ");
                ((IAppendableElemant)Source).AppendTo(sb, dialect);
            }

            foreach (var joinCommand in JoinClauses)
            {
                sb.Append(" ");
                ((IAppendableElemant)joinCommand).AppendTo(sb, dialect);
            }

            if (WhereClause != null)
            {
                sb.Append(" ");
                ((IAppendableElemant)WhereClause).AppendTo(sb, dialect);
            }
            if (GroupByClause != null)
            {
                sb.Append(" ");
                ((IAppendableElemant)GroupByClause).AppendTo(sb, dialect);
            }
            if (OrderByClause != null)
            {
                sb.Append(" ");
                ((IAppendableElemant)OrderByClause).AppendTo(sb, dialect);
            }
            if (LimitClause != null)
            {
                sb.Append(" ");
                ((IAppendableElemant)LimitClause).AppendTo(sb, dialect);
            }
        }

        //public IAcceptsJoin Join(TableOrSubQuery source, string constraint)
        //{
        //    this.Accept(this.Source.Join(source, constraint));
        //    return this;
        //}

        //public IAcceptsJoin Join(string table, string constraint, string alias)
        //{
        //    this.Accept(this.Source.Join(table, constraint, alias));
        //    return this;
        //}

        //public IAcceptsGroupBy Where(string expression)
        //{
        //    this.Accept(new WhereClause(expression));
        //    return this;
        //}

        //public IAcceptsOrderBy GroupBy(params string[] termArgs)
        //{
        //    return GroupBy((IEnumerable<string>)termArgs);
        //}

        //public IAcceptsOrderBy GroupBy(IEnumerable<string> terms)
        //{
        //    this.Accept(new GroupByClause(terms));
        //    return this;
        //}

        //public IAcceptsLimit OrderBy(IEnumerable<string> terms)
        //{
        //    this.Accept(new OrderByClause(terms));
        //    return this;
        //}

        //public IAcceptsLimit OrderBy(params string[] termArgs)
        //{
        //    this.Accept(new OrderByClause(termArgs));
        //    return this;
        //}

        //public ISelectElement Limit(int limit, int offset)
        //{
        //    this.Accept(new LimitClause(limit, offset));
        //    return this;
        //}

        //public void Accept(ISelectElement parent)
        //{
        //    throw new NotSupportedException("select can't have parent");
        //}

        public void Accept(JoinClause joinClause)
        {
            JoinClauses.Add(joinClause);
        }

        public void Accept(WhereClause whereClause)
        {
            if (WhereClause != null) { WhereClause += whereClause; }
            else { WhereClause = whereClause; }
        }

        public void Accept(GroupByClause groupByClause)
        {
            if (GroupByClause != null) { GroupByClause += groupByClause; }
            else { GroupByClause = groupByClause; }
        }

        public void Accept(OrderByClause orderByClause)
        {
            if (OrderByClause != null) { OrderByClause += orderByClause; }
            else { OrderByClause = orderByClause; }
        }

        public void Accept(LimitClause limitClause)
        {
            LimitClause = limitClause;
        }
    }
}