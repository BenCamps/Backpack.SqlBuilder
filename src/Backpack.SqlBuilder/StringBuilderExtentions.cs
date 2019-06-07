
using System.Collections.Generic;
using System.Text;

namespace Backpack.Sqlbuilder
{
    internal static class StringBuilderExtentions
    {
#if NetCF
        public static StringBuilder AppendLine(this StringBuilder @this)
        {
            @this.Append("\r\n");
            return @this;
        }

        public static StringBuilder AppendLine<TValue>(this StringBuilder @this, TValue value)
        {
            @this.Append(value);
            @this.Append("\r\n");
            return @this;
        }
#endif

        public static StringBuilder Join(this StringBuilder @this, string seporator, IEnumerable<string> elements)
        {
            var first = false;
            foreach(var e in elements)
            {
                if(first == false) { @this.Append(seporator); }
                else { first = false; }
                @this.Append(e);
            }
            return @this;
        }
    }
}
