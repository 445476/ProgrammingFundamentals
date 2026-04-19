namespace BLL.Exceptions
{
    public class BlException : Exception
    {
        public BlException(string message, Exception? inner = null)
            : base(message, inner)
        {
        }
    }
}