﻿namespace Backpack.SqlBuilder
{
    public interface IAcceptsOrderBy : IAcceptsLimit
    {
        void Accept(OrderByClause orderByClause);
    }
}