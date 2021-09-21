using VideogameStorage.Models;
using VideogameStorage.Extensions;

using Microsoft.AspNetCore.JsonPatch;

using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VideogameStorage.Services
{
    public class VideogameService
    {
        
        private readonly ApplicationDbContext _db;
        private readonly ILogger<VideogameService> _logger;

        public VideogameService(ILogger<VideogameService> logger, ApplicationDbContext videogameContext)
        {
            _logger = logger;
            _db = videogameContext;
            if (_db.Videogames.Any()) return;
            DatabaseInitilazition.InitData(_db);
        }

        public async Task<List<Videogame>> GetAllVideogamesAsync()
        {
            Console.WriteLine("--------------");
            Console.WriteLine("Listing all videogames in the database");
            Console.WriteLine("--------------");

            var videogames = await _db.Videogames
                                 .OrderBy(vg => vg.VideogameId)
                                 .ToListAsync();
            return videogames;
        }

        public async Task<PagingResult<Videogame>> GetVideogamesAsync(int offset = 0, int limit = 15)
        {
            Console.WriteLine("--------------");
            Console.WriteLine("Listing videogames in the database");
            Console.WriteLine("--------------");

            var totalRecords = await _db.Videogames.CountAsync();
            var videogames = await _db.Videogames
                                 .OrderBy(vg => vg.VideogameId)
                                 .Skip(offset)
                                 .Take(limit)
                                 .ToListAsync();
            return new PagingResult<Videogame>(videogames, totalRecords);
        }


        public async Task<Videogame> GetVideogameAsync(int videogameId)
        {

            Console.WriteLine("--------------");
            Console.WriteLine("List videogame by [" + videogameId +"] id");
            Console.WriteLine("--------------");

            return await _db.Videogames
                                 .SingleOrDefaultAsync(vg => vg.VideogameId == videogameId);
        }

        public async Task<Videogame> CreateVideogameAsync(Videogame videogame)
        {
            _db.Add(videogame);
            try
            {
              await _db.SaveChangesAsync();
            }
            catch (System.Exception exp)
            {
               _logger.LogError($"Error in {nameof(CreateVideogameAsync)}: " + exp.Message);
            }

            return videogame;
        }

        public async Task<bool> UpdateVideogameAsync(Videogame videogame)
        {

            _db.Videogames.Attach(videogame);
            _db.Entry(videogame).State = EntityState.Modified;
            try
            {
                if( await _db.SaveChangesAsync() > 0)
                {
                    Console.WriteLine("--------------");
                    Console.WriteLine("Updating videogame by [" + videogame.VideogameId +"] id, where the modified data is: \n" + videogame);
                    Console.WriteLine("--------------");
                    return true;
                }
                else
                    return false;
            }
            catch (Exception exp)
            {
                _logger.LogError($"Error in {nameof(UpdateVideogameAsync)}: " + exp.Message);
            }
            return false;

        }

        public async Task<bool> DeleteVideogameAsync(int videogameId)
        {
            var videogame = await _db.Videogames
                                .SingleOrDefaultAsync(vg => vg.VideogameId == videogameId);
            _db.Remove(videogame);
            try
            {
                if( await _db.SaveChangesAsync() > 0)
                {
                    Console.WriteLine("--------------");
                    Console.WriteLine("Deleted videogame by [" + videogameId +"] id");
                    Console.WriteLine("--------------");
                    return true;
                }
                else
                    return false;
            }
            catch (System.Exception exp)
            {
               _logger.LogError($"Error in {nameof(DeleteVideogameAsync)}: " + exp.Message);
            }
            return false;
        }


    }

    

}