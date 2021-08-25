using VideogameStorage.Models;
using VideogameStorage.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;

namespace VideogameStorage.Services
{
    public class VideogameService
    {
        
        private readonly VideogameContext _db;

        public VideogameService(VideogameContext videogameContext)
        {
            _db = videogameContext;
            if (_db.Videogames.Any()) return;
            DatabaseInitilazition.InitData(_db);
        }

        public Videogame CreateVideogame(Videogame videogame)
        {
            
            _db.Add(videogame);
            _db.SaveChanges();

            Console.WriteLine("--------------");
            Console.WriteLine("Creating a new videogame: \n" + videogame);
            Console.WriteLine("--------------");

            return videogame;
        }

        public IQueryable<Videogame> GetAllVideogamesByType(String gameType="")
        {
            var videogameList = _db.Videogames as IQueryable<Videogame>;

            if(!string.IsNullOrEmpty(gameType))
            {
                videogameList = videogameList.Where(vg => vg.Type.StartsWith(gameType));
            }

            // Just for console logging the list
            var myString = "";
            var sb = new System.Text.StringBuilder();
            foreach (Videogame vg in videogameList)
            {
                sb.Append(vg).Append(",\n");
            }
            myString = sb.Remove(sb.Length - 2, 2).ToString();
            //

            Console.WriteLine("--------------");
            Console.WriteLine("Listing videogames in the database: \n" + myString);
            Console.WriteLine("--------------");
            return videogameList;
        }


        public Videogame GetVideogameById(int videogameId)
        {
            

            var videogame = _db.Videogames.FirstOrDefault(vg => vg.VideogameId == videogameId);

            Console.WriteLine("--------------");
            Console.WriteLine("Listing videogame by [" + videogameId +"] id: \n" + videogame);
            Console.WriteLine("--------------");

            return videogame;
        }

        public Videogame Update(Videogame videogame)
        {
            

            var vg = _db.Videogames.First(vg => vg.VideogameId == videogame.VideogameId);
            vg = videogame;
            _db.SaveChanges();

            Console.WriteLine("--------------");
            Console.WriteLine("Updating videogame by [" + videogame.VideogameId +"] id, where the modified data is: \n" + videogame);
            Console.WriteLine("--------------");

            return videogame;
        }
        
        public void Delete(int id)
        {
            

            var videogame = _db.Videogames.FirstOrDefault(vg => vg.VideogameId == id);
            

            _db.Remove(_db.Videogames.Single(vg => vg.VideogameId == id));
            _db.SaveChanges();

            Console.WriteLine("--------------");
            Console.WriteLine("Deleting videogame by [" + id +"] id, where the deleted data is: \n" + videogame);
            Console.WriteLine("--------------");
        }

    }

    

}