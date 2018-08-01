namespace Backpack.SqlBuilder
{
    public interface IAcceptsLimit
    {
        void Accept(LimitClause limitClause);
    }
}