using System.Text.Json;
using Tamagochi.Interfaces;
using Tamagochi.Modules;

namespace Tamagochi.BL;

static class FileManager
{
    private static string fileName = "Cats.json";
    public static void CheckOrCreateFile()
    {
        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate));
     
    }
    public static void SaveAll(List<Cat> allCats)
    {
        File.WriteAllText(fileName, " ");
        foreach(Cat cat in allCats)
        {
            SaveOne(cat);
        }
        
    }
    public static void SaveOne(Cat cat)
    {
        string serCat = JsonSerializer.Serialize<Cat>(cat);
        File.AppendAllText(fileName, serCat + "\n");

    }

    public static List<Cat> ReadAll()
    {
        string[] allCatsString = File.ReadAllLines(fileName);
        List<Cat> allCats = new List<Cat>();
        foreach (string cat in allCatsString)
        {
            Cat? catDesir = JsonSerializer.Deserialize<Cat>(cat);
            if (catDesir != null)
            {
                allCats.Add(catDesir);
            }
        }
        return allCats;
    }
    public static void ReadOne()
    {
        // TODO: Прочитать из файла
    }
    public static void Sorting()
    {
        // TODO: Показать таблицу на экране
    }
}
