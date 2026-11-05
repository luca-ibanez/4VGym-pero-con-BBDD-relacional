using _4VGym_pero_con_BBDD_relacional.Dtos;
using _4VGym_pero_con_BBDD_relacional.Models;

namespace _4VGym_pero_con_BBDD_relacional.Repositories
{
    public class ActivityRepository
    {
        private Dictionary<long, Instructor> _activities = new();
        private readonly ActivityTypeRepository _repositoryActivityType;
        private readonly InstructorRepository _repositoryInstructor;

        private int _nextId = 1; //Esto es para el autonumerico, TODO: comprobar si es necesario en esta version.

        public ActivityRepository(ActivityTypeRepository repositoryActivityType, InstructorRepository repositoryInstructor)
        {
            _repositoryActivityType = repositoryActivityType;
            _repositoryInstructor = repositoryInstructor;
        }

        public async Task<Activity> Add(Activity activity)
        {
            //TODO: Hacer todos los chequeos aqui
        }
    }
}
