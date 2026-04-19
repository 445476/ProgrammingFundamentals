using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSimulator
{
    //Helpers and interfaces
    
    public interface IMultiplayer
    {
        void StartMultiplayer(Device device);
    }

    public interface ICastable     //interface for phones
    {
        void CastTo(Device targetDevice);
    }

    public enum OSPlatform { WindowsPC, MacOS, Android, iOS }   // which platforms it could be

    public class HardwareSpecs   //specs both for games and devices
    {
        public int CpuCores { get; set; }
        public int RamGb { get; set; }

        public bool MeetsRequirements(HardwareSpecs min)
        {
            return CpuCores >= min.CpuCores && RamGb >= min.RamGb;
        }
    }

    // Hardware

    public class Device
    {
        public string Name { get; set; }
        public OSPlatform OS { get; set; }
        public HardwareSpecs Specs { get; set; }
        public int FreeStorageGb { get; set; }
        public int ConnectedManipulators { get; set; }         //controllers for playing multiplayer
        
        
        //game-related things
        public List<Game> InstalledGames { get; } = new List<Game>();
        public Game ActiveGame { get; private set; }  //game thats played rn
        public Dictionary<Game, int> SavedStates { get; } = new Dictionary<Game, int>();         //dictionary so games could be saved on diff devices

        public event EventHandler<string> OnNotification;

        protected void Notify(string message) => OnNotification?.Invoke(this, $"[{Name}] {message}");

        //start a game
        public bool TrySetActiveGame(Game game)
        {
            if (ActiveGame != null && ActiveGame != game)
            {
                Notify($"Cant load {game.Title}. {ActiveGame.Title} is already started.");
                return false;
            }
            ActiveGame = game;
            return true;
        }

        //turnoff game
        public void ClearActiveGame()
        {
            ActiveGame = null;
        }

        public void SaveGameProgress(Game game)
        {
            if (!SavedStates.ContainsKey(game)) SavedStates[game] = 0;
            SavedStates[game]++;
        }

        public int GetSavedStatesCount(Game game)
        {
            return SavedStates.ContainsKey(game) ? SavedStates[game] : 0;
        }
    }

    public class MobileDevice : Device, ICastable
    {
        public void CastTo(Device targetDevice)
        {
            Notify($"Started casting on {targetDevice.Name}");
        }
    }


    //"business" logic

    public abstract class Game
    {
        public string Title { get; set; }
        public HardwareSpecs MinRequirements { get; set; }
        public int SizeGb { get; set; }

        public event EventHandler OnStateChanged;
        protected void TriggerStateChange() => OnStateChanged?.Invoke(this, EventArgs.Empty);

        public virtual void Install(Device device)
        {
            if (device.InstalledGames.Contains(this)) return; 
            if (device.FreeStorageGb < SizeGb) return;

            device.FreeStorageGb -= SizeGb;
            device.InstalledGames.Add(this); 
            TriggerStateChange();
        }

        public virtual void Run(Device device, bool isLoginSuccessful)
        {
            if (!isLoginSuccessful) return; 
            if (!device.InstalledGames.Contains(this)) return; 
            if (!device.Specs.MeetsRequirements(MinRequirements)) return; 
            
            device.TrySetActiveGame(this);
            TriggerStateChange();
        }

        public void Save(Device device)
        {
            if (device.ActiveGame != this) return; 
            device.SaveGameProgress(this); 
            TriggerStateChange();
        }

        public virtual void Load(Device device)
        {
            if (device.ActiveGame != this) return;
            if (device.GetSavedStatesCount(this) == 0) return;
            
            TriggerStateChange();
        }

        public virtual void Close(Device device)
        {
            if (device.ActiveGame == this)
            {
                device.ClearActiveGame();
                TriggerStateChange();
            }
        }
    }

    public class StrategyGame : Game
    {
        public override void Run(Device device, bool isLoginSuccessful)
        {
            if (device.OS != OSPlatform.WindowsPC) 
            {
                return; 
            }

            base.Run(device, isLoginSuccessful);
        }
    }

    public class AdventureGame : Game { }

    public class RpgGame : Game, IMultiplayer
    {
        public bool IsMultiplayerActive { get; private set; }

        public void StartMultiplayer(Device device)
        {
            if (device.ConnectedManipulators < 2) return; 
            
            IsMultiplayerActive = true;
            TriggerStateChange();
        }

        public override void Close(Device device)
        {
            base.Close(device);
            IsMultiplayerActive = false;
        }
    }


    //console UI


    class Program
    {
        static List<Device> devices = new List<Device>();
        static List<Game> globalGameCatalog = new List<Game>();
        static Device currentDevice = null;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            InitializeData();

            while (true)
            {
                Console.Clear();
                if (currentDevice == null) ShowDeviceSelection();
                else ShowDeviceMenu();
            }
        }

        static void InitializeData()
        {
            var strongMac = new Device //can run anything
            { 
                Name = "Stronk Mac", OS = OSPlatform.MacOS, FreeStorageGb = 40, 
                Specs = new HardwareSpecs { CpuCores = 16, RamGb = 32 },
                ConnectedManipulators = 2
            };
            
            var weakWin = new Device //can run strategy and rpg
            { 
                Name = "Wek WindowsPC", OS = OSPlatform.WindowsPC, FreeStorageGb = 60, 
                Specs = new HardwareSpecs { CpuCores = 4, RamGb = 8 },
                ConnectedManipulators = 1
            };

            var strongPhone = new MobileDevice //can run rpg
            { 
                Name = "Strong IAndroid", OS = OSPlatform.Android, FreeStorageGb = 20, 
                Specs = new HardwareSpecs { CpuCores = 8, RamGb = 12 },
                ConnectedManipulators = 0
            };
            
            devices.Add(strongMac);
            devices.Add(weakWin);
            devices.Add(strongPhone);

            //game init
            globalGameCatalog.Add(new StrategyGame 
            { 
                Title = "SunCraft (Strategy)", SizeGb = 20, 
                MinRequirements = new HardwareSpecs { CpuCores = 2, RamGb = 4 },
            });
            
            globalGameCatalog.Add(new RpgGame 
            { 
                Title = "The Witch (RPG)", SizeGb = 20, 
                MinRequirements = new HardwareSpecs { CpuCores = 4, RamGb = 8 } 
            });
            
            globalGameCatalog.Add(new AdventureGame 
            { 
                Title = "Tomb Charted (Adventure))", SizeGb = 20, 
                MinRequirements = new HardwareSpecs { CpuCores = 8, RamGb = 16 }
            });
        }

        static void ShowDeviceSelection()
        {
            Console.WriteLine("=== Choose a device ===");
            for (int i = 0; i < devices.Count; i++)
                Console.WriteLine($"{i + 1}. {devices[i].Name} ({devices[i].OS})");
            Console.WriteLine("0. Exit");

            Console.Write("\nDo it: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0) Environment.Exit(0);
                if (choice > 0 && choice <= devices.Count) currentDevice = devices[choice - 1];
            }
        }

        static void ShowDeviceMenu()
        {
            // Виведення характеристик та стану пристрою
            Console.WriteLine($"=== DEVICE: {currentDevice.Name} ===");
            Console.WriteLine($"OS: {currentDevice.OS} | Processor: {currentDevice.Specs.CpuCores} cores | RAM: {currentDevice.Specs.RamGb} ГБ");
            Console.WriteLine($"Freeeeee Spaaaaceeee: {currentDevice.FreeStorageGb} GB | Controllers: {currentDevice.ConnectedManipulators}");
            
            Console.WriteLine("\n-- Installed games --");
            if (currentDevice.InstalledGames.Count == 0) Console.WriteLine(" (None)");
            foreach (var game in currentDevice.InstalledGames) Console.WriteLine($" - {game.Title}");
            
            Console.WriteLine("---------------------------------------------------");
            
            if (currentDevice.ActiveGame != null)
                Console.WriteLine($"[Game started!]: {currentDevice.ActiveGame.Title}\n1. Open game menu");
            else
                Console.WriteLine("1. Start Game");

            Console.WriteLine("2. Install Game");
            Console.WriteLine("3. Change controllers count");
            
            if (currentDevice is MobileDevice)
                Console.WriteLine("4. Cast the game");
            
            Console.WriteLine("9. Choose Device");

            Console.Write("\nYour choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (currentDevice.ActiveGame != null) ManageActiveGame();
                    else RunInstalledGame();
                    break;
                case "2": ShowCatalog(); break;
                case "3":
                    Console.Write("How many controllers? (0-4): ");
                    if (int.TryParse(Console.ReadLine(), out int pads)) currentDevice.ConnectedManipulators = pads;
                    break;
                case "4":
                    if (currentDevice is MobileDevice mobile)
                    {
                        Console.WriteLine("\n[SYS] Game casted on another device.");
                        Console.ReadLine();
                    }
                    break;
                case "9": currentDevice = null; break;
            }
        }

        static void ShowCatalog()
        {
            Console.Clear();
            Console.WriteLine("=== Games ===");
            for (int i = 0; i < globalGameCatalog.Count; i++)
            {
                var game = globalGameCatalog[i];
                string status = currentDevice.InstalledGames.Contains(game) ? "[Already installed]" : $"[{game.SizeGb} GB]";
                Console.WriteLine($"{i + 1}. {game.Title} {status}");
            }
            Console.WriteLine("0. Back");

            Console.Write("\nChoose game: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= globalGameCatalog.Count)
            {
                var game = globalGameCatalog[choice - 1];
                double initialStorage = currentDevice.FreeStorageGb;
                
                game.Install(currentDevice);
                
                if (currentDevice.InstalledGames.Contains(game) && initialStorage != currentDevice.FreeStorageGb)
                    Console.WriteLine("\n[Peremoga] Game installed!");
                else
                    Console.WriteLine("\n[Zrada] Not enough space.");
                Console.ReadLine();
            }
        }

        static void RunInstalledGame()
        {
            if (currentDevice.InstalledGames.Count == 0) return;

            Console.Clear();
            Console.WriteLine("=== Starting game ===");
            for (int i = 0; i < currentDevice.InstalledGames.Count; i++)
                Console.WriteLine($"{i + 1}. {currentDevice.InstalledGames[i].Title}");
            Console.WriteLine("0. Back");

            Console.Write("\nChoose game: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= currentDevice.InstalledGames.Count)
            {
                var selectedGame = currentDevice.InstalledGames[choice - 1];
                
                Console.Write("\nEnter the password to login(write 1 for success): ");
                bool isAuth = Console.ReadLine() == "1";

                selectedGame.Run(currentDevice, isAuth);
                
                if (currentDevice.ActiveGame == selectedGame)
                    ManageActiveGame();
                else
                {
                    Console.WriteLine("\n[ERR] Aborted launch. Possible causes:");
                    Console.WriteLine("- Wrong password");
                    Console.WriteLine("- Weak Hardware");
                    Console.WriteLine("- Wrong OS");
                    Console.ReadLine();
                }
            }
        }

        static void ManageActiveGame()
        {
            var game = currentDevice.ActiveGame;

            while (currentDevice.ActiveGame == game)
            {
                Console.Clear();
                Console.WriteLine($"=== Playing: {game.Title} ===");
                Console.WriteLine($"Saves: {currentDevice.GetSavedStatesCount(game)}");
                
                if (game is RpgGame rpgGame && rpgGame.IsMultiplayerActive)
                    Console.WriteLine("Status: Multiplayer enabled");

                Console.WriteLine("\n1. Save");
                Console.WriteLine("2. Load");
                
                if (game is RpgGame)
                    Console.WriteLine("3. Start Multiplayer");
                
                Console.WriteLine("0. Close game");

                Console.Write("\nYour choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": 
                        game.Save(currentDevice); 
                        Console.WriteLine("\n[SYS] Saved.");
                        Console.ReadLine();
                        break;
                    case "2": 
                        if (currentDevice.GetSavedStatesCount(game) > 0)
                        {
                            game.Load(currentDevice); 
                            Console.WriteLine("\n[SYS] Loaded.");
                        }
                        else
                        {
                            Console.WriteLine("\n[ERR] Not enough saves.");
                        }
                        Console.ReadLine();
                        break;
                    case "3": 
                        if (game is RpgGame rpg) 
                        {
                            rpg.StartMultiplayer(currentDevice);
                            if(!rpg.IsMultiplayerActive) 
                                Console.WriteLine("\n[ERR] Connect at least 2 gamepads!");
                            else
                                Console.WriteLine("\n[SYS] Multiplayer multiplayin!");
                            Console.ReadLine();
                        }
                        break;
                    case "0": 
                        game.Close(currentDevice); 
                        break;
                }
            }
        }
    }
}