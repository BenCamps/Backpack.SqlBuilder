using System.Text;

namespace Backpack.SqlBuilder
{
    public interface IAppendableElemant
    {
        //void AppendTo(StringBuilder builder);

        void AppendTo(StringBuilder sb, ISqlDialect dialect);
    }
}