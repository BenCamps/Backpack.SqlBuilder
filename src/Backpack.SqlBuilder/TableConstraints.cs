using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backpack.SqlBuilder
{
    public abstract class TableConstraint : CommandElement
    {
        public string ConstraintName { get; set; }
    }

    internal class GenericConstraint : TableConstraint
    {
        public GenericConstraint(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Value { get; set; }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (!string.IsNullOrEmpty(ConstraintName)) { sb.Append("CONSTRAINT ").Append(ConstraintName).Append(" "); }
            sb.Append(Value);
        }
    }

    public class UniqueConstraint : TableConstraint
    {
        public IEnumerable<ColumnInfo> Columns { get; set; }
        public IEnumerable<string> ColumnNames { get; set; }

        public string ConflictOption { get; set; }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (!string.IsNullOrEmpty(ConstraintName)) { sb.Append("CONSTRAINT ").Append(ConstraintName).Append(" "); }
            sb.Append("UNIQUE ( ");
            if (Columns != null)
            {
                bool first = true;
                foreach (var col in Columns)
                {
                    if (!first) { sb.Append(", "); }
                    else { first = false; }
                    sb.Append(col.Name);
                }
            }
            else if (ColumnNames != null)
            {
                bool first = true;
                foreach (var col in ColumnNames)
                {
                    if (!first) { sb.Append(", "); }
                    else { first = false; }
                    sb.Append(col);
                }
            }
            else
            { throw new InvalidOperationException("either Columns or ColumnNames must be set"); }
            sb.Append(")");

            if (!string.IsNullOrEmpty(ConflictOption))
            {
                sb.Append(" ON CONFLICT ").Append(ConflictOption);
            }
        }
    }

    public class PrimaryKeyConstraint : TableConstraint
    {
        public IEnumerable<ColumnInfo> Columns { get; set; }
        public IEnumerable<string> ColumnNames { get; set; }

        public string ConflictOption { get; set; }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (!string.IsNullOrEmpty(ConstraintName)) { sb.Append("CONSTRAINT ").Append(ConstraintName).Append(" "); }
            sb.Append("PRIMARY KEY ( ");
            if (Columns != null)
            {
                bool first = true;
                foreach (var col in Columns)
                {
                    if (!first) { sb.Append(", "); }
                    else { first = false; }
                    sb.Append(col.Name);
                }
            }
            else if (ColumnNames != null)
            {
                bool first = true;
                foreach (var col in ColumnNames)
                {
                    if (!first) { sb.Append(", "); }
                    else { first = false; }
                    sb.Append(col);
                }
            }
            else
            { throw new InvalidOperationException("either Columns or ColumnNames must be set"); }
            sb.Append(")");

            if (!string.IsNullOrEmpty(ConflictOption))
            {
                sb.Append(" ON CONFLICT ").Append(ConflictOption);
            }
        }
    }

    public class CheckConstraint : TableConstraint
    {
        public string Expression { get; set; }

        public CheckConstraint(string expression)
        { Expression = expression; }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            sb.Append("CHECK (").Append(Expression).Append(")");
        }
    }

    public class ForeignKeyConstraint : TableConstraint
    {
        public IEnumerable<ColumnInfo> Columns { get; set; }
        public IEnumerable<string> ColumnNames { get; set; }

        public string References { get; set; }

        public IEnumerable<string> ReferenceColumnNames { get; set; }

        public ForeignKeyTriggers OnDelete { get; set; }

        public ForeignKeyTriggers OnUpdate { get; set; }

        public Deferrable Deferrable { get; set; }

        public string ForeignKeyClause { get; set; }

        protected override void AppendTo(StringBuilder sb, ISqlDialect dialect)
        {
            if (!string.IsNullOrEmpty(ConstraintName)) { sb.Append("CONSTRAINT ").Append(ConstraintName).Append(" "); }
            sb.Append("FOREIGN KEY ( ");
            if (Columns != null)
            {
                bool first = true;
                foreach (var col in Columns)
                {
                    if (!first) { sb.Append(", "); }
                    else { first = false; }
                    sb.Append(col.Name);
                }
            }
            else if (ColumnNames != null)
            {
                bool first = true;
                foreach (var col in ColumnNames)
                {
                    if (!first) { sb.Append(", "); }
                    else { first = false; }
                    sb.Append(col);
                }
            }
            else
            { throw new InvalidOperationException("either Columns or ColumnNames must be set"); }
            sb.Append(")");

            if (ForeignKeyClause != null)
            {
                sb.Append(ForeignKeyClause + " ");
            }
            else
            {
                sb.Append("REFERENCES " + References + " ");
                if (ReferenceColumnNames != null && ReferenceColumnNames.Any())
                {
                    sb.Append("( ");
                    var first = true;
                    foreach (var col in ReferenceColumnNames)
                    {
                        if (!first) { sb.Append(", "); }
                        else { first = false; }
                        sb.Append(col);
                    }
                    sb.Append(") ");
                }
            }

            var onUpdate = dialect.ConvertToString(OnUpdate);
            if (onUpdate != null)
            {
                sb.Append("ON UPDATE " + onUpdate);
            }

            var onDelete = dialect.ConvertToString(OnDelete);
            if (onDelete != null)
            {
                sb.Append("ON DELETE " + onUpdate);
            }

            var deferable = dialect.ConvertToString(Deferrable);
            if (deferable != null)
            {
                sb.Append(deferable);
            }
        }
    }
}