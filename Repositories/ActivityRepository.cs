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
            List<ErrorResponse> errorResponse; //TODO: Poner esto en el out, descubrir como sacarlo.
            errorResponse = new List<ErrorResponse>();
            //Chequear si existe el tipo de actividad
            if (_repositoryActivityType.GetById(activity.ActivityTypeId) == null)
            {
                errorResponse.Add(new ErrorResponse(1, "La actividad ya existe."));
            }
            //Chequear que existen todos los monitores
            foreach (Instructor instructor in activity.Instructors)
            {
                if (_repositoryInstructor.GetById(instructor.Id) == null)
                {
                    errorResponse.Add(new ErrorResponse(35, "El instructor: " + instructor.Id + " no existe."));
                }
            }

            //Chequear que la fecha sea correcta
            if (activity.DateStart < DateTime.Today || activity.DateEnd < DateTime.Today || activity.DateStart > activity.DateEnd)
            {
                errorResponse.Add(new ErrorResponse(36, "Las fechas son incorrectas."));
            }

            //Chequear que el maximo de participantes sea mayor que 0

            if (activity.MaxParticipants < 0)
            {
                errorResponse.Add(new ErrorResponse(37, "El numero de participantes debe ser mayor que 0."));
            }

            if (errorResponse.Count > 0)
            {
                return null; //TODO: Hacer que esto pueda devolver el errorResponse, ya que ahora no puede.
            } else
            {
                return null;
            }
        }
    }
}
