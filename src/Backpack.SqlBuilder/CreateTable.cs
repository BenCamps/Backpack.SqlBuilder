using System;
using System.Collections.Generic;
using System.Text;

namespace Backpack.SqlBuilder
{
    public class CreateTable : SqlCommandBuilder
    {
        public bool Temp { get; set; }
        public bool IfNotExists { get; set; }

        public string SchemaName { get; set; }
        public string TableName { get; set; }

        public ICollection<ColumnInfo> Columns { get; set; } = new List<ColumnInfo>();

        public ICollection<TableConstraint> TableConstraints { get; set; } = new List<TableConstraint>();

        public bool WithoutRowid { get; set; }

        public SqlSelectBuilder SelectStatment { get; set; }

        public CreateTable()
        {
        }

        public CreateTable(ISqlDialect dialect) : base(dialect)
        {
        }

        public override string ToString()
        {
            return ToString(Dialect ?? SqlDialect.DefaultDialect);
        }

        public void WithColumns(IEnumerable<ColumnInfo> columns)
        {
            foreach (var c in columns)
            {
                Columns.Add(c);
            }
        }

        public override void AppendTo(StringBuilder sb)
        {
            var dialect = Dialect;

            sb.Append("CREATE");
            if (Temp) { sb.Append(" TEMP"); }
            sb.Append(" TABLE ");
            if (IfNotExists) { sb.Append("IF NOT EXISTS "); }
            if (!string.IsNullOrEmpty(SchemaName)) { sb.Append(SchemaName).Append("."); }
            sb.Append(TableName);

            if (SelectStatment != null)
            {
                sb.Append(" AS ");
                SelectStatment.AppendTo(sb);
            }
            else
            {
                sb.Append(" (");
                bool first = true;
                foreach (var col in Columns)
                {
                    if (!first) { sb.Append(", "); }
                    else { first = false; }
                    sb.Append(dialect.GetColumnDefinition(col));
                }

                if (TableConstraints != null)
                {
                    foreach (var constr in TableConstraints)
                    {
                        if (!first) { sb.Append(", "); }
                        else { first = false; }
                        constr.AppendTo(sb, dialect);
                    }
                }

                sb.Append(")");

                if (WithoutRowid) { sb.Append(" WITHOUT ROWID"); }
            }
        }
    }

    public static class CreateTableExtentions
    {
        public static CreateTable WithTableConstraints(this CreateTable @this, IEnumerable<string> tableConstraints)
        {
            foreach (var tc in tableConstraints)
            {
                @this.TableConstraints.Add(new GenericConstraint(tc));
            }

            return @this;
        }

        public static CreateTable WithTableConstraints(this CreateTable @this, IEnumerable<TableConstraint> tableConstraints)
        {
            foreach (var tc in tableConstraints)
            {
                @this.TableConstraints.Add(tc);
            }

            return @this;
        }

        public static CreateTable PrimaryKey(this CreateTable @this, string columnName)
        {
            @this.TableConstraints.Add(new PrimaryKeyConstraint() { ColumnNames = new string[] { columnName } });
            return @this;
        }

        public static CreateTable ForeignKey(this CreateTable @this,
            IEnumerable<string> columnNames,
            string references,
            IEnumerable<string> referencesColumnNames = null,
            ForeignKeyTriggers onDelete = default(ForeignKeyTriggers),
            ForeignKeyTriggers onUpdate = default(ForeignKeyTriggers),
            Deferrable deferrable = default(Deferrable)
            )
        {
            if (referencesColumnNames == null) { referencesColumnNames = columnNames; }

            @this.TableConstraints.Add(new ForeignKeyConstraint()
            {
                ColumnNames = columnNames,
                ReferenceColumnNames = referencesColumnNames,
                OnDelete = onDelete,
                OnUpdate = onUpdate,
                Deferrable = deferrable,
            });

            return @this;
        }

        public static CreateTable Unique(this CreateTable @this, string columnName, string onConflict = null)
        {
            if (columnName == null) { throw new ArgumentNullException("columnName"); }
            @this.TableConstraints.Add(new UniqueConstraint() { ColumnNames = new string[] { columnName }, ConflictOption = onConflict });

            return @this;
        }

        public static CreateTable Check(this CreateTable @this, string expression)
        {
            if (expression == null) { throw new ArgumentNullException("expression"); }

            @this.TableConstraints.Add(new CheckConstraint(expression));
            return @this;
        }
    }
}