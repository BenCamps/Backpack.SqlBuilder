namespace Backpack.SqlBuilder
{
    public enum ForeignKeyTriggers { NoAction = 0, Cascade, Restrict, SetDefault, SetNull };
    public enum Deferrable { Default = 0, NotDeferrable, Deferrable, InitiallyDeferred, InitiallyImmediate };

    public enum OnConflictOption { Default, Rollback, Abort, Fail, Ignore, Replace };
}