using _4VGym_pero_con_BBDD_relacional.Data;
using _4VGym_pero_con_BBDD_relacional.Dtos;
using _4VGym_pero_con_BBDD_relacional.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _4VGym_pero_con_BBDD_relacional.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {
        //Recordar hacer estas otras dos cosas. La primera es para crear una propiedad llamada _repository
        private readonly GymDbContext _repository;
        // TOLEARN: Aqui basicamente hacemos que se mire a GymDbContext, ya que lo usaremos en vez del active repository
        public ActivityController(GymDbContext repository)
        {
            _repository = repository;
        }
        // Si despues de hacer lo de arriba no funciona y dice algo de que no hay servicio, eso es porque hay que ir a Program.cs ir poner:
        // builder.Services.AddSingleton<GymDbContext>();
        // Esto hace que solo pueda haber 1 objeto unico de cada clase.

        /// <summary>
        /// POST /activities
        /// Añade una nueva actividad en 4VGym
        /// </summary>
        /// <param name="activity">ObjetoActivity enviado en el cuerpo de la peticion (JSON)</param>
        [HttpPost]
        [ProducesResponseType(typeof(Activity), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Activity>> CreateActivity([FromBody] Activity activity)
        {
            try
            {

                //Activity? activityCreated = _repository.Add(activity, out List<ErrorResponse> errors);
                // TOLEARN: Aqui no puedo hacer el .Add ya que tendre que usar la base de datos SQL para añadir, y no puede tener un add asi
                // Eso significa que tendre que seguramente hacer toda la validacion que antes se hacia en el Repository aqui en vez

                //Chequear si ya existe (Por ID)
                if (_repository.ActivityTypes.Contains(activity.ActivityType))
                {
                    return BadRequest(new ErrorResponse(1, "La actividad ya existe."));
                }

                //Chequear que existan todos los monitores
                var idsDeInstructoresPasador = activity.Instructors.Select(i => i.Id).Distinct().ToList();
                // TOLEARN: Aqui devuelve una lista despues de coger los IDs de los instructores de la lista de instructores del activity que nos dio, .Distinct para que todos los cogidos sean unicos
                var idsDeInstructoresExistentes = await _repository.Instructors.Where(i => idsDeInstructoresPasador.Contains(i.Id)).Select(i => i.Id).ToListAsync();
                // TOLEARN: Aqui devuelve una lista de todos los instructores que tienen esa ID

                if (idsDeInstructoresExistentes.Count != idsDeInstructoresPasador.Count)
                {
                    return BadRequest(new ErrorResponse(2, "Hay instructores puestos que no existen en la base de datos"));
                }
                //Chequear que la fecha sea correcta
                if (activity.DateStart > activity.DateEnd) {
                    return BadRequest(new ErrorResponse(3, "La fecha inicial es mayor que la fecha final."));
                }

                //Ahora despues de todos los filtros para comprobar que todo esta bien, habria que añadirlo.
                //Pero segun el trabajo no hemos visto inserciones en Bases de datos a si que no lo insertamos.
                //_repository.Activities.Add(activity);


                // EN caso de OK
                return StatusCode(StatusCodes.Status201Created, activity);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new ErrorResponse(1, "An internal server error occurred"));

            }

        }

    }
}
