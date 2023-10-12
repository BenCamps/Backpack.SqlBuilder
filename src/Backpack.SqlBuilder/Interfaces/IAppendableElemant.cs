using System.Text;

namespace Backpack.SqlBuilder
{
    public interface IAppendableElemant
    {
        void AppendTo(StringBuilder sb, ISqlDialect dialect);
    }
}