using _4VGym_pero_con_BBDD_relacional.Data;

namespace _4VGym_pero_con_BBDD_relacional.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _context;

        public ActivityTypeRepository(GymDbContext context)
        {
            _context = context;
        }
    }
}
