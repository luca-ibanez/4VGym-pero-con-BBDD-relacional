using _4VGym_pero_con_BBDD_relacional.Data;
using _4VGym_pero_con_BBDD_relacional.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGym_pero_con_BBDD_relacional.Repositories
{
    public class InstructorRepository
    {
        private readonly GymDbContext _context;
        public InstructorRepository(GymDbContext context) {
            _context = context;
        }

        //Para obtener todos
        public IEnumerable<Instructor> GetAll()
        {
            return _context.Instructors;
        }

        //Para obtener por ID
        public async Task<Instructor?> GetById(int id)
        {
            return await _context.Instructors.FirstOrDefaultAsync(i => i.Id == id);
        }
        //Para obtener por nombre
        public async Task<Instructor?> GetByName(string nombre)
        {
            return await _context.Instructors.SingleOrDefaultAsync(i => i.Name == nombre);
        } //TOLEARN: Task<Instructor?> es para que cuando termine de un objeto Instructor o null. await es para que el programa espere a que acabe la operacion
        //Para obtener por email
        public async Task<Instructor?> GetByEmail(string email)
        {
            return await _context.Instructors.SingleOrDefaultAsync(i => i.Email == email);
        }



    }
}
