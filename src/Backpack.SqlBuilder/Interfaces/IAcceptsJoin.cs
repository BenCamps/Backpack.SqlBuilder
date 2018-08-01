namespace Backpack.SqlBuilder
{
    public interface IAcceptsJoin : IAcceptsWhere
    {
        void Accept(JoinClause joinClause);
    }
}