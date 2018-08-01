namespace Backpack.SqlBuilder
{
    public interface IAcceptsGroupBy : IAcceptsOrderBy
    {
        void Accept(GroupByClause groupByClause);
    }
}