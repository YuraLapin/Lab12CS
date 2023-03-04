using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Text;

namespace Lab12Main
{
    public class Program
    {
        public const string ERRMSG = "Выбран неверный пункт";
        public const string LINE = "----------------------------------------------------------------------------";
        public const string MISSINGMSG = "Элемент отсутствует в списке";
        public const string SUCCESSMSG = "Элемент успешно удален";
        public const int nameSize = 10;

        public static Random rand = new Random();
        public static CycledList<Transport> list = new CycledList<Transport>();

        public static string RandomWord(int size, string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789")
        {
            var sb = new StringBuilder();
            for (int i = 0; i < size; ++i)
            {
                sb.Append(alphabet[rand.Next() % alphabet.Count()]);
            }
            return sb.ToString();
        }

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
                            string name = RandomWord(nameSize);
                            int power = rand.Next() % 1000;
                            list.Add(new Transport(name, power));
                            break;
                        }
                    case 2:
                        {
                            string name = RandomWord(nameSize);
                            int power = rand.Next() % 1000;
                            int cars = rand.Next() % 25;
                            list.Add(new Train(name, power, cars));
                            break;
                        }
                    case 3:
                        {
                            string name = RandomWord(nameSize);
                            int power = rand.Next() % 1000;
                            int cars = rand.Next() % 25;
                            var stations = new List<string>();
                            int count = rand.Next() % 5 + 1;
                            for (int i = 0; i < count; ++i)
                            {
                                stations.Add("station" + i);
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
            Console.Write("Введите имя для удаления: ");
            string name = Console.ReadLine();
            int power = UserInteractor.GetIntFromUser("Введите мощность для удаления", true);
            if (!list.Remove(new Transport(name, power)))
            {
                Console.WriteLine(MISSINGMSG);
            }
            else
            {
                Console.WriteLine(SUCCESSMSG);
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