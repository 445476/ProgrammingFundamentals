using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringLibDLL
{
    public class BigLetters : Strings
    {
        private string data;

        public BigLetters(string str)
        {
            data = str;
        }

        public override int Length()
        {
            return (Encoding.UTF8.GetByteCount("B") * SymbCount());
        }

        public override int SymbCount()
        {
            int count = 0;
            foreach (char c in data)
            {
                if (c == 'B') count++;
            }
            return count;
        }
    }
}
