using System;

//wasnt sure where to put helper methods so made another class with them. their purpose is to check input

namespace PL
{
    public static class Helper
    {
        public static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                Console.WriteLine("Invalid number. Try again.");
            }
        }

        public static string ReadString(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s))
                    return s;
                Console.WriteLine("Input cannot be empty.");
            }
        }

        public static DateTime ReadDate(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (DateTime.TryParse(Console.ReadLine(), out var date))
                    return date;
                Console.WriteLine("Invalid date format.");
            }
        }

        public static TimeSpan ReadTime(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (TimeSpan.TryParse(Console.ReadLine(), out var time))
                    return time;
                Console.WriteLine("Invalid time format.");
            }
        }
    }
}