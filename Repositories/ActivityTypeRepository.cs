using _4VGym_pero_con_BBDD_relacional.Data;
using _4VGym_pero_con_BBDD_relacional.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGym_pero_con_BBDD_relacional.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _context;

        public ActivityTypeRepository(GymDbContext context)
        {
            _context = context;
        }

        //Para obtener todos los tipos de actividades
        public IEnumerable<ActivityType> GetAll()
        {
            return _context.ActivityTypes;
        }

        //Para obtener por ID
        public async Task<ActivityType?> GetById(int id)
        {
            return await _context.ActivityTypes.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
