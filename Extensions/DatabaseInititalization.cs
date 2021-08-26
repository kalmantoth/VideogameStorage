
using System;
using VideogameStorage.Models;

namespace VideogameStorage.Extensions
{
    public static class DatabaseInitilazition
    {
    public static void InitData(ApplicationDbContext context)
    {
        var rnd = new Random();

        var namePrefixes = new [] { "World of", "Project", "Fine", 
            "Fight", "Super", "Killer", "Big", "Small", "Nice", 
            "True", "Dirty", "Yeeter", "Colorful"};

        var namePostfixes = new [] { "Kitten", "Dragon", "Rekt","Fighter",
            "Bob", "Tree", "Leaf", "Node", "Road", "Coal", "Fox", "Troll", "Shark"};
    
        var gameTypes = new [] { "MMO", "RPG", "Open World", "Shooter", "Battle Royal",
            "Adventure", "Hack and Slash", "Puzzle", "RTS", "Action" , "Rythm", "Survival",
             "Horror", "Rougelike", "Racing", "Party", "Idle", "Casual" };
        var materials = new [] { "Steel", "Wooden", "Concrete", "Plastic",
                                        "Granite", "Rubber" };
        var names = new [] { "Chair", "Car", "Computer", "Pants", "Shoes" };
        var departments = new [] { "Books", "Movies", "Music", 
                                        "Games", "Electronics" };
    
        context.Videogames.AddRange(1000.Times(x =>
        {
        var namePrefix = namePrefixes[rnd.Next(0, namePrefixes.Length)];
        var namePostfix = namePostfixes[rnd.Next(0, namePostfixes.Length)];
        var type = gameTypes[rnd.Next(0, gameTypes.Length)];
        
        
        var material = materials[rnd.Next(0, 5)];
        var name = names[rnd.Next(0, 5)];
        var department = departments[rnd.Next(0, 5)];
        var productId = $"{x, -3:000}";

    
        return new Videogame
        {
            VideogameId = 0,
            Name = $"{namePrefix} {namePostfix}",
            Type = type,
            ReleaseDate = rnd.Next(1985, 2021),
            Rating = (float) rnd.Next(10, 50) / 10,
            ConsoleExclusive = rnd.NextDouble() >= 0.70
            
        };
        }));

        context.SaveChanges();
    }
    }
}
