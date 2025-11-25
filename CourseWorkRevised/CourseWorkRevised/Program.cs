using BLL.Services;

namespace PL
{
    public class Program
    {
        public static void Main()
        {
            var service = new EntityService();
            var menu = new MainMenu(service);
            menu.Show();
        }
    }
}