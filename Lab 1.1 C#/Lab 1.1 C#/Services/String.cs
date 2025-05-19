using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1._1Cs.Services
{
    public class Striing
    {
        private string data;

        public Striing()
        {

        }

        public Striing(string input)
        {
            setString(input);
        }

        ~Striing()
        {
            Console.Write("Destructor has been called");
        }

        public Striing(Striing copy)
        {
            setString(copy.data);
        }

        public void setString(string input)
        {
            data = input;
        }

        public string getString()
        {
            return data;
        }

        public int measureString()
        {
            //can be done through data.Lenght
            int length = 0;
            foreach (char c in data)
            {
                length++;
            }
            return length;
        }

        public string sortString()
        {
            char[] sorted = data.ToArray();
            Array.Sort(sorted);
            Array.Reverse(sorted);
            return new string(sorted);
        }

    }
}