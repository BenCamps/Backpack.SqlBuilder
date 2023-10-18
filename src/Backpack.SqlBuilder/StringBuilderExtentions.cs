using System.Collections.Generic;
using System.Text;

namespace Backpack.SqlBuilder
{
    internal static class StringBuilderExtentions
    {
#if !NET6_0_OR_GREATER
        public static StringBuilder AppendJoin(this StringBuilder @this, string separator, IEnumerable<string> elements)
        {
            var first = true;
            foreach (var e in elements)
            {
                if (first == false) { @this.Append(separator); }
                else { first = false; }
                @this.Append(e);
            }
            return @this;
        }
#endif
    }
}