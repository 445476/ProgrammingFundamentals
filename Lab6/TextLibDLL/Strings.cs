namespace TextLibDLL
{
    public interface IReplacer
    {
        void ReplaceChar(char oldChar, char newChar);
    }

    public class Strings:IReplacer
    {
        public string Line { get; private set; }

        private int Length;

        public Strings(string line)
        {
            Line = line;
            Length = line.Length;
        }

        public int GetLenght()
        {
            return Length;
        }

        public void ReplaceChar(char oldChar, char newChar)
        {
            Line = Line.Replace(oldChar, newChar);
        }
    }
}
