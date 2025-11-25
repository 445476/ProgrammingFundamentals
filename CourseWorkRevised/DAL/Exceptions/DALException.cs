using System;

namespace DAL.Exceptions
{
    /// <summary>
    /// Exception thrown inside the data access layer.
    /// </summary>
    public class DalException : Exception
    {
        public DalException(string message, Exception? inner = null)
            : base(message, inner)
        {
        }
    }
}