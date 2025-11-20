using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace Program
{
    // Creating class 
    public class Product : IComparable<Product>
    {
        public string Name { get; set; }
        public string Code { get; set; } 
        public DateTime ManufactureDate { get; set; }
        public int ShelfLifeDays { get; set; } // how much days it can go before expire

        // default constructor
        public Product() { }

        //parametrized constructor
        public Product(string name, string code, DateTime manufactureDate, int shelfLifeDays)
        {
            Name = name;
            Code = code;
            ManufactureDate = manufactureDate;
            ShelfLifeDays = shelfLifeDays;
        }


        // finding the date at which the product will expire
        public DateTime GetExpirationDate()
        {
            return ManufactureDate.AddDays(ShelfLifeDays);
        }

        // check if expired, either take the date from class or take the date from today and compare to expdate
        public bool IsConsumable(DateTime? atDate = null)
        {
            var check = atDate ?? DateTime.Today;
            return check.Date <= GetExpirationDate().Date;
        }

        // ovrriding tostring so compiler correctly shows objects
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "Product(Name={0}, Code={1}, Mfg={2:yyyy-MM-dd}, ShelfLife={3}d, Exp={4:yyyy-MM-dd})",
                Name, Code, ManufactureDate, ShelfLifeDays, GetExpirationDate());
        }

        // IComparable realization
        public int CompareTo(Product other)
        {
            if (other == null) return 1;
            return string.Compare(this.Code, other.Code, StringComparison.OrdinalIgnoreCase);
        }

        // method for finding product by code
        public bool HasCode(string code) => string.Equals(Code, code, StringComparison.OrdinalIgnoreCase);
    }

    // Generic BT, that accepts ONLY class which does Icomparable
    public class BinaryTree<T> : IEnumerable<T> where T : class, IComparable<T>
    {
        private class Node
        {
            public T Value;
            public Node Left;
            public Node Right;
            public Node(T value) { Value = value; }
        }

        private Node root;

        public BinaryTree() { }

        // recursively add element
        public void Add(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            root = AddRecursive(root, value);
        }

        private Node AddRecursive(Node node, T value)
        {
            if (node == null) return new Node(value);
            int cmp = value.CompareTo(node.Value);
            if (cmp < 0)
                node.Left = AddRecursive(node.Left, value);
            else
                node.Right = AddRecursive(node.Right, value);
            return node;
        }

        // remove first instance of object
        public bool Remove(T value)
        {
            bool removed;
            root = RemoveRecursive(root, value, out removed);
            return removed;
        }

        private Node RemoveRecursive(Node node, T value, out bool removed)
        {
            removed = false;
            // node not found
            if (node == null) return null;
            int cmp = value.CompareTo(node.Value);

            //traversing the tree
            if (cmp < 0)
                node.Left = RemoveRecursive(node.Left, value, out removed);
            else if (cmp > 0)
                node.Right = RemoveRecursive(node.Right, value, out removed);

            //node was found
            else
            {
                removed = true;
                // if node with 0-1 children
                if (node.Left == null) return node.Right;
                if (node.Right == null) return node.Left;
                // ifnode with 2 children we need to change the tree
                Node change = MinNode(node.Right);
                node.Value = change.Value;
                node.Right = RemoveRecursive(node.Right, change.Value, out _);
            }
            return node;
        }

        private Node MinNode(Node node)
        {
            while (node.Left != null) node = node.Left;
            return node;
        }

        // preorder traversal (root left right)

        // init iterator

        // generic
        public IEnumerator<T> GetEnumerator()
        {
            return new PreorderEnumerator(root);
        }

        // non-generic
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // realisation of ienumerator
        private class PreorderEnumerator : IEnumerator<T>
        {
            private Stack<Node> stack = new Stack<Node>();
            private Node start;
            public PreorderEnumerator(Node root)
            {
                start = root;
                if (root != null) stack.Push(root);
            }

            private T current;
            public T Current => current;
            

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                //check if there are still elements
                if (stack.Count == 0)
                {
                    current = null;
                    return false;
                }

                var node = stack.Pop();
                current = node.Value;

                // push right first so left is processed first 
                if (node.Right != null) stack.Push(node.Right);
                if (node.Left != null) stack.Push(node.Left);

                return true;
            }

            //return stack to starting pos
            public void Reset()
            {
                stack.Clear();
                if (start != null) 
                    stack.Push(start);
            }

            // exists solely for realisation of interface
            public void Dispose() { }
        }

        // predicate is used so we can find element using every field of product
        public T Find(Predicate<T> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return FindRecursive(root, predicate);
        }

        private T FindRecursive(Node node, Predicate<T> predicate)
        {
            if (node == null) 
                return null;
            if (predicate(node.Value)) 
                return node.Value;
            //cheking left side
            var left = FindRecursive(node.Left, predicate);
            if (left != null) return left;
            //checking right side
            return FindRecursive(node.Right, predicate);
        }
    }

    // main
    public static class Program
    {
        static void Main()
        {
            // create and manipulate array
            Console.WriteLine("Array:");
            Product[] arr = new Product[5];
            arr[0] = new Product("Milk", "1", new DateTime(2025, 9, 1), 30);
            arr[1] = new Product("Bread", "2", new DateTime(2025, 10, 12), 7);
            arr[2] = new Product("Cheese", "3", new DateTime(2025, 8, 20), 120);
            arr[3] = new Product("Yogurt", "4", new DateTime(2025, 11, 1), 14);
            arr[4] = new Product("Juice", "5", new DateTime(2025, 7, 15), 365);

            foreach (var p in arr) Console.WriteLine("  " + p);

            // change
            Console.WriteLine("\nChanging shelf life of 2 to 10 days");
            for (int i = 0; i < arr.Length; i++)
                if (arr[i].HasCode("2")) arr[i].ShelfLifeDays = 10;
            Console.WriteLine("Updated array:");
            foreach (var p in arr) Console.WriteLine("  " + p);

            // search
            Console.WriteLine("\nSearch for product with id 3");
            var foundArr = Array.Find(arr, x => x.HasCode("3"));
            Console.WriteLine(foundArr != null ? " Product: " + foundArr : " Product not found");

            // delete (changing element to null)
            Console.WriteLine("\nDelete product with id 5");
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] != null && arr[i].HasCode("5")) arr[i] = null;
            Console.WriteLine("Redacted array:");
            foreach (var p in arr) Console.WriteLine("  " + (p == null ? "<null>" : p.ToString()));

            Console.WriteLine("\n\n Non-generic array list:");
            ArrayList al = new ArrayList();
            al.Add(new Product("Sausage", "10", new DateTime(2025, 6, 1), 60));
            al.Add(new Product("Butter", "11", new DateTime(2025, 9, 15), 180));
            al.Add(new Product("Eggs", "12", new DateTime(2025, 11, 5), 21));

            foreach (var obj in al) Console.WriteLine("  " + obj);

            // remove
            Console.WriteLine("\nRemove Butter from ArrayList");
            for (int i = 0; i < al.Count; i++)
            {
                if (al[i] is Product px && px.HasCode("11"))
                {
                    al.RemoveAt(i);
                    break;
                }
            }
            Console.WriteLine("After removal:");
            foreach (var obj in al) Console.WriteLine("  " + obj);

            // update
            Console.WriteLine("\nUpdate Eggs shelf life to 28 days");
            for (int i = 0; i < al.Count; i++)
            {
                if (al[i] is Product px && px.HasCode("12")) px.ShelfLifeDays = 28;
            }
            foreach (var obj in al) Console.WriteLine("  " + obj);

            Console.WriteLine("\n\n Generic list<product>:");
            List<Product> list = new List<Product>
            {
                new Product("Honey", "20", new DateTime(2024, 1, 1), 3650),
                new Product("Tea", "21", new DateTime(2023, 5, 5), 3650),
                new Product("Coffee", "22", new DateTime(2024, 6, 10), 3650)
            };

            list.ForEach(p => Console.WriteLine("  " + p));

            // Add, Remove, Update, Search
            Console.WriteLine("\nAdd Sugar");
            list.Add(new Product("Sugar", "23", new DateTime(2025, 1, 1), 3650));
            Console.WriteLine("After add:");
            list.ForEach(p => Console.WriteLine("  " + p));

            Console.WriteLine("\nFind 21:");
            var foundList = list.Find(p => p.HasCode("21"));
            Console.WriteLine(foundList != null ? " Product: " + foundList : " Product not found");

            Console.WriteLine("\nUpdate Coffee name");
            var idxCoffee = list.FindIndex(p => p.HasCode("22"));
            if (idxCoffee >= 0) list[idxCoffee].Name = "Lab3.2 Coffee";
            list.ForEach(p => Console.WriteLine("  " + p));

            Console.WriteLine("\nRemove honey");
            list.RemoveAll(p => p.HasCode("20"));
            list.ForEach(p => Console.WriteLine("  " + p));

            // sort list
            Console.WriteLine("\nSort List by code");
            list.Sort();
            list.ForEach(p => Console.WriteLine("  " + p));

            // BST
            Console.WriteLine("\nBST:");
            BinaryTree<Product> tree = new BinaryTree<Product>();
            tree.Add(new Product("Roshen", "100", new DateTime(2025, 1, 1), 100));
            tree.Add(new Product("Milka", "050", new DateTime(2025, 2, 1), 90));
            tree.Add(new Product("WD40", "150", new DateTime(2025, 3, 1), 120));
            tree.Add(new Product("AK-47", "075", new DateTime(2025, 4, 1), 60));
            tree.Add(new Product("BadDragon", "125", new DateTime(2025, 5, 1), 30));

            Console.WriteLine("Traversing tree:");
            foreach (var p in tree)
            {
                Console.WriteLine("  " + p);
            }

            // search tree
            Console.WriteLine("\nFind AK-47 in tree:");
            var foundInTree = tree.Find(x => x.HasCode("075"));
            Console.WriteLine(foundInTree != null ? " Product: " + foundInTree : " Product not found");

            // remove
            Console.WriteLine("\nRemove Roshen");
            var toRemove = new Product("", "100", DateTime.Today, 1); // creating product with the same code to compare it with Roshen
            bool removed = tree.Remove(toRemove);
            Console.WriteLine("Removed? " + removed);
            Console.WriteLine("Tree after removal:");
            foreach (var p in tree) Console.WriteLine("  " + p);

            // checking for expiration
            Console.WriteLine("\nExpiration check");
            var checkProduct = new Product("Milk2", "01", DateTime.Today.AddDays(-10), 7);
            Console.WriteLine(checkProduct);
            Console.WriteLine("Consumable today? " + (checkProduct.IsConsumable() ? "Yes" : "No"));
            Console.WriteLine("Expiration date: " + checkProduct.GetExpirationDate().ToString("yyyy-MM-dd"));
        }
    }
}
