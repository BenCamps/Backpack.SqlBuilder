namespace Backpack.SqlBuilder
{
    public interface IAcceptsWhere : IAcceptsGroupBy
    {
        void Accept(WhereClause where);
    }
}