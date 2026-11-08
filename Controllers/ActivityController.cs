using _4VGym_pero_con_BBDD_relacional.Data;
using _4VGym_pero_con_BBDD_relacional.Dtos;
using _4VGym_pero_con_BBDD_relacional.Models;
using _4VGym_pero_con_BBDD_relacional.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _4VGym_pero_con_BBDD_relacional.Controllers
{
    [ApiController]
    [Route("activities")]
    public class ActivityController : ControllerBase
    {
        //Recordar hacer estas otras dos cosas. La primera es para crear una propiedad llamada _repository
        private readonly ActivityRepository _repository;

        public ActivityController(ActivityRepository repository)
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
        public async Task<ActionResult<Activity>> CreateActivityAsync([FromBody] Activity activity)
        {
            try
            {

                Activity? activityCreated = await _repository.Add(activity);

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
