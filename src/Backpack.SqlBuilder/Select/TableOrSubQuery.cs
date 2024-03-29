﻿using System;
using System.Text;

namespace Backpack.SqlBuilder
{
    public class TableOrSubQuery : CommandElement
    {
        public string Table { get; set; }
        public SqlSelectBuilder SubQuery { get; set; }
        public string Alias { get; set; }
        public string Joins { get; set; }

        public string SourceName
        {
            get
            {
                return Table ?? Alias;
            }
        }

        public TableOrSubQuery()
        {
        }

        public TableOrSubQuery(string tableName) : this()
        {
            Table = tableName;
        }

        public TableOrSubQuery(String tableName, string alias) : this(tableName)
        {
            this.Alias = alias;
        }

        public TableOrSubQuery(SqlSelectBuilder subQuery, string alias)
            : this()
        {
            this.SubQuery = subQuery;
            this.Alias = alias;
        }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (!string.IsNullOrEmpty(Table)) { sb.Append(Table); }
            else if (SubQuery != null)
            {
                sb.Append("(");
                ((IAppendableElemant)SubQuery).AppendTo(sb, dialect);
                sb.Append(")");
            }

            if (!String.IsNullOrEmpty(Alias))
            { sb.Append(" AS " + Alias); }

            if (!string.IsNullOrEmpty(Joins))//support legacy join style
            {
                sb.Append(" ").Append(Joins);
            }
        }
    }
}