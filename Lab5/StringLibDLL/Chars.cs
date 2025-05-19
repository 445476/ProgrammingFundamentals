using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringLibDLL
{
    public class Chars : Strings
    {
        private string data;

        public Chars(string str)
        {
            data = str;
        }

        public override int Length()
        {
            return (Encoding.UTF8.GetByteCount("*") * SymbCount());
        }

        public override int SymbCount()
        {
            int count = 0;
            foreach (char c in data)
            {
                if (c == '*') count++;
            }
            return count;
        }
    }
}
