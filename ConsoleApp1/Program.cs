using System.Text;
using Tamagochi.BL;
using Tamagochi.Modules;

namespace Tamagochi;

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        FileManager.CheckOrCreateFile();

        /*Cat cat = InputData();
        FileManager.SaveOne(cat);*/

        Menu menu = new Menu();
        while (true)
        {
            switch (menu.Run())
            {
                case 0:
                    FileManager.SaveOne(InputData());
                    break;
                case 1:
                    ActionCat("Покормить", 1);
                    Console.WriteLine("Вы покормили кота");
                    break;
                case 2:
                    ActionCat("Поиграть с котом", 2);
                    Console.WriteLine("Вы поиграли с котом");
                    break;
                case 3:
                    ActionCat("Вылечить кота", 3);
                    Console.WriteLine("Вы полечили кота");
                    break;
                case 4:
                    NextDay();
                    Console.WriteLine("Следующий день");
                    break;
                case 5:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
            Console.Clear();
        }
    }

    public static void ActionCat(string action, int actionIndex)
    {
        string nameCat = UserInput.InputString("Выберите кота по имени");

        List<Cat> allCats = FileManager.ReadAll();
        bool findCat = false;
        foreach (Cat cat in allCats)
        {
            if (cat.Name.Contains(nameCat))
            {
                Console.WriteLine($"{action}: {nameCat}");

                switch (actionIndex)
                {
                    case 1:
                        if (cat.Age <= 5)
                        {
                            cat.Satiety += 7;
                            cat.Mood += 7;
                        }
                        else if (cat.Age >= 6 && cat.Age <= 10)
                        {
                            cat.Satiety += 5;
                            cat.Mood += 5;
                        }
                        else
                        {
                            cat.Satiety += 4;
                            cat.Mood += 4;
                        }
                        break;
                    case 2:
                        if (cat.Age <= 5)
                        {
                            cat.Mood += 7;
                            cat.Health += 7;
                            cat.Satiety -= 3;
                        }
                        else if (cat.Age >= 6 && cat.Age <= 10)
                        {
                            cat.Mood += 5;
                            cat.Health += 5;
                            cat.Satiety -= 5;
                        }
                        else
                        {
                            cat.Mood += 4;
                            cat.Health += 4;
                            cat.Satiety -= 6;
                        }
                        break;
                    case 3:
                        if (cat.Age <= 5)
                        {
                            cat.Mood -= 7;
                            cat.Health += 7;
                            cat.Satiety -= 3;

                        }
                        else if (cat.Age >= 6 && cat.Age <= 10)
                        {
                            cat.Mood -= 5;
                            cat.Health += 5;
                            cat.Satiety -= 5;
                        }
                        else
                        {
                            cat.Mood -= 6;
                            cat.Health += 4;
                            cat.Satiety -= 6;
                        }
                        break;
                }

                findCat = true;
            }
            cat.Level = (cat.Health + cat.Mood + cat.Satiety) / 3;
        }
        FileManager.SaveAll(allCats); 
        if (!findCat)
        {
            Console.WriteLine("нет такого кота");
        }
        Console.ReadKey(true);
    }
    public static void NextDay()
    {
        List<Cat> cats = FileManager.ReadAll();
        Random random = new Random();
        foreach(Cat cat in cats)
        {
            cat.Health += random.Next(-3, 4);
            cat.Satiety += random.Next(1, 6);
            cat.Mood += random.Next(-3, 4);
        }
        FileManager.SaveAll(cats);
        
    }
    public static Cat InputData()
    {
        Cat cat = new Cat();
        cat.Name = UserInput.InputString("Введи Имя");
        cat.Age = UserInput.InputInteger("Введи возраст");
        Random random = new Random();
        cat.Health = random.Next(20, 81);
        cat.Mood = random.Next(20, 81);
        cat.Satiety = random.Next(20, 81);
        cat.Level = (cat.Health + cat.Mood + cat.Satiety) / 3;
        return cat;
    }
    
}