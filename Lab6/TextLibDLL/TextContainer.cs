using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TextLibDLL
{
    public class TextContainer
    {
        private List<Strings> text = new();

        public void AddLine(string line)
        {
            text.Add(new Strings(line));
        }

        public string GetLine(int index)
        {
            return text[index].Line;
        }
        public void RemoveLine(int index)
        {
            if (index >= 0 && index < text.Count)
                text.RemoveAt(index);
        }

        public void Delete()
        {
            text.Clear();
        }

        public int TotalCharacters()
        {
            int total = 0;
            
            for (int i = 0; i < text.Count; i++)
            {
                total += text[i].GetLenght();
            }
            
            return total;
        }

        public int FindString(string search)
        {
            int total = 0;
            
            for (int i = 0; i < text.Count; i++)
            {
                if (text[i].Line == search)
                {
                    total++;
                };
            }


            return total;
        }

        public void ReplaceCharInText(char oldChar, char newChar)
        {
            foreach (var line in text)
            {
                line.ReplaceChar(oldChar, newChar);
            }
        }
    }
}
