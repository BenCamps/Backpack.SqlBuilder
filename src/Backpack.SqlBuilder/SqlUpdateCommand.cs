using System;
using System.Collections.Generic;
using System.Text;

namespace Backpack.SqlBuilder
{
    public class SqlUpdateCommand : SqlCommandBuilder
    {
        public SqlUpdateCommand(ISqlDialect dialect = null) : base(dialect)
        {
        }

        public SqlUpdateCommand(string tableName, OnConflictOption conflictOption = OnConflictOption.Default, ISqlDialect dialect = null) : base(dialect)
        {
            TableName = tableName ?? throw new ArgumentNullException(nameof(tableName));
            ConflictOption = conflictOption;
        }

        public string TableName { get; set; }
        public OnConflictOption ConflictOption { get; set; }

        public IList<string> ColumnNames { get; set; }

        public IList<string> ValueExpressions { get; set; }

        public IEnumerable<KeyValuePair<string, object>> Values { get; set; }

        public WhereClause WhereClause { get; set; }

        public ICompleatedSqlStatment Where(string whereClause)
        {
            WhereClause = new WhereClause(whereClause);
            return this;
        }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            sb.AppendLine("UPDATE");
            if (ConflictOption != OnConflictOption.Default)
            { sb.AppendLine("OR " + ConflictOption.ToString()); }
            sb.AppendLine(TableName);

            if (Values != null)
            {
                bool first = true;
                foreach (var pair in Values)
                {
                    if (!first) { sb.AppendLine(","); }
                    sb.Append("SET ").Append(pair.Key).Append(" = ").Append(pair.Value.ToString());
                }
            }
            else
            {
                for (int i = 0; i < ColumnNames.Count; i++)
                {
                    if (i == 0) { sb.AppendLine("SET"); }
                    else { sb.Append(", "); }
                    sb.AppendLine(ColumnNames[i] + " = " + ValueExpressions[i]);
                }
            }

            if (WhereClause != null)
            {
                ((IAppendableElemant)WhereClause).AppendTo(sb, dialect);
                sb.AppendLine();
            }
        }
    }
}