using System.ComponentModel.Design;
using System.Linq.Expressions;

namespace Lab12Main
{
    public class Program
    {
        public const string ERRMSG = "Выбран неверный пункт";
        public const string LINE = "----------------------------------------------------------------------------";
        public const string MISSINGMSG = "Элемент отсутствует в списке";
        public const string SUCCESSMSG = "Элемент успешно удален";

        public static Random rand = new Random();
        public static CycledList list = new CycledList();

        public static void AddElement()
        {
            bool extFlag = false;
            while (!extFlag)
            {
                Console.WriteLine(LINE);
                Console.WriteLine(" 1 - Добавить Transport");
                Console.WriteLine(" 2 - Добавить Train");
                Console.WriteLine(" 3 - Добавить Express");
                Console.WriteLine(" 4 - Назад");
                int ans = UserInteractor.GetIntFromUser("Выберите действие [1-4]");
                switch (ans)
                {
                    case 1:
                        {
                            Console.Write("Введите имя: ");
                            string name = Console.ReadLine();
                            int power = UserInteractor.GetIntFromUser("Введите мощность", true);
                            list.Add(new Transport(name, power));
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите имя: ");
                            string name = Console.ReadLine();
                            int power = UserInteractor.GetIntFromUser("Введите мощность", true);
                            int cars = UserInteractor.GetIntFromUser("Введите количество вагонов", true);
                            list.Add(new Train(name, power, cars));
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Введите имя: ");
                            string name = Console.ReadLine();
                            int power = UserInteractor.GetIntFromUser("Введите мощность", true);
                            int cars = UserInteractor.GetIntFromUser("Введите количество вагонов", true);
                            var stations = new List<string>();
                            int count = UserInteractor.GetIntFromUser("Введите количество станций", true);
                            for (int i = 0; i < count; ++i)
                            {
                                Console.Write("Введите название станции: ");
                                stations.Add(Console.ReadLine());
                            }
                            list.Add(new Express(name, power, cars, stations));
                            break;
                        }
                    case 4:
                        {
                            extFlag = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(ERRMSG);
                            break;
                        }
                }
            }
        }

        public static void DelElement()
        {
            bool extFlag = false;
            while (!extFlag)
            {
                Console.WriteLine(LINE);
                Console.WriteLine(" 1 - Удалить Transport");
                Console.WriteLine(" 2 - Удалить Train");
                Console.WriteLine(" 3 - Удалить Express");
                Console.WriteLine(" 4 - Назад");
                int ans = UserInteractor.GetIntFromUser("Выберите действие [1-4]");
                switch (ans)
                {
                    case 1:
                        {
                            Console.Write("Введите имя: ");
                            string name = Console.ReadLine();
                            int power = UserInteractor.GetIntFromUser("Введите мощность", true);
                            if (!list.Remove(new Transport(name, power)))
                            {
                                Console.WriteLine(MISSINGMSG);
                            }
                            else
                            {
                                Console.WriteLine(SUCCESSMSG);
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Введите имя: ");
                            string name = Console.ReadLine();
                            int power = UserInteractor.GetIntFromUser("Введите мощность", true);
                            int cars = UserInteractor.GetIntFromUser("Введите количество вагонов", true);
                            if (!list.Remove(new Train(name, power, cars)))
                            {
                                Console.WriteLine(MISSINGMSG);
                            }
                            else
                            {
                                Console.WriteLine(SUCCESSMSG);
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Введите имя: ");
                            string name = Console.ReadLine();
                            int power = UserInteractor.GetIntFromUser("Введите мощность", true);
                            int cars = UserInteractor.GetIntFromUser("Введите количество вагонов", true);
                            var stations = new List<string>();
                            int count = UserInteractor.GetIntFromUser("Введите количество станций", true);
                            for (int i = 0; i < count; ++i)
                            {
                                Console.Write("Введите название станции: ");
                                stations.Add(Console.ReadLine());
                            }
                            if(!list.Remove(new Express(name, power, cars, stations)))
                            {
                                Console.WriteLine(MISSINGMSG);
                            }
                            else
                            {
                                Console.WriteLine(SUCCESSMSG);
                            }
                            break;
                        }
                    case 4:
                        {
                            extFlag = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(ERRMSG);
                            break;
                        }
                }
            }
        }

        public static void Menu()
        {
            bool extFlag = false;

            while (!extFlag)
            {
                Console.Clear();
                list.Print();
                Console.WriteLine(" 1 - Добавить элемент в список");
                Console.WriteLine(" 2 - Удалить элемент из списка");
                Console.WriteLine(" 3 - Выход");
                int ans = UserInteractor.GetIntFromUser("Выберите действие [1-3]");
                switch (ans)
                {
                    case 1:
                        {
                            AddElement();
                            break;
                        }
                    case 2:
                        {
                            DelElement();
                            break;
                        }
                    case 3:
                        {
                            extFlag = true;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine(ERRMSG);
                            break;
                        }
                }
            }
        }

        public static int Main()
        {
            Menu();

            return 0;
        }
    }
}