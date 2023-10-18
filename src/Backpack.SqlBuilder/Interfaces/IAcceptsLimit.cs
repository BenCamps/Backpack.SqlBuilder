namespace Backpack.SqlBuilder
{
    public interface IAcceptsLimit : ICompleatedSqlStatment
    {
        void Accept(LimitClause limitClause);
    }
}