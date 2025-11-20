using System;

namespace lab
{
    // delegate
    public delegate string CompareSymbols(string input);

    //arg class
    public class ListClearedEventArgs : EventArgs
    {
        //when happened
        public DateTime Time { get; }
        
        //what happened
        public string Message { get; }

        //constructor
        public ListClearedEventArgs(string message)
        {
            Time = DateTime.Now;
            Message = message;
        }
    }

    // component which we are modelling
    public class ListModel
    {
        // event: list was cleared
        public event EventHandler<ListClearedEventArgs>? ListCleared;

        public void ClearList()
        {
            OnListCleared("List was cleared");
        }
        private void OnListCleared(string msg)
        {
            ListCleared?.Invoke(this, new ListClearedEventArgs(msg));
        }


    }

    // event handler

    public class EventHandler {
        private static void OnListClearedHandler(object? sender, ListClearedEventArgs e)
        {
            Console.WriteLine($"\nEvent: {e.Message}");
            Console.WriteLine($"Time: {e.Time}");
            Console.WriteLine($"Initiated by: {sender?.GetType().Name}");
        }
    class Program
    {
        static void Main(string[] args)
        {

            //anonymous method
            CompareSymbols anon = delegate (string s)
            {
                int letters = 0, digits = 0;

                foreach (char c in s)
                {
                    if (char.IsLetter(c)) letters++;
                    else if (char.IsDigit(c)) digits++;
                }

                if (letters > digits) return "Anon: more letter";
                if (digits > letters) return "Anon: more numbers";
                return "Anon: equal";
            };

            //lambda
            CompareSymbols lambda = (string s) =>
            {
                int letters = 0, digits = 0;

                foreach (char c in s)
                {
                    if (char.IsLetter(c)) letters++;
                    else if (char.IsDigit(c)) digits++;
                }

                if (letters > digits) return "Lambda: more letter";
                if (digits > letters) return "Lambda: more numbers";
                return "Lambda: equal";
            };

            Console.Write("Enter string: ");
            string input = Console.ReadLine() ?? "";

            Console.WriteLine(anon(input));
            Console.WriteLine(lambda(input));

            //event
            ListModel model = new ListModel();
            model.ListCleared += OnListClearedHandler;

            Console.WriteLine("\nClearing List");
            model.ClearList();
        }

       
        }
    }
}