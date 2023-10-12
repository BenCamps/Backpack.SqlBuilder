
using System.Collections.Generic;
using System.Text;

namespace Backpack.Sqlbuilder
{
    internal static class StringBuilderExtentions
    {
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
