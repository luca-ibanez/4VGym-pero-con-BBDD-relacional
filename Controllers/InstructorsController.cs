using _4VGym_pero_con_BBDD_relacional.Data;
using _4VGym_pero_con_BBDD_relacional.Dtos;
using _4VGym_pero_con_BBDD_relacional.Models;
using _4VGym_pero_con_BBDD_relacional.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4VGym_pero_con_BBDD_relacional.Controllers
{
    [ApiController]
    [Route("instructors")]
    public class InstructorsController : ControllerBase
    {
        private readonly InstructorRepository _repository;

        // Inyectamos la clase concreta del repositorio
        public InstructorsController (InstructorRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// GET /instructor
        /// GET /instructor?first_initial=M
        /// Devuelve el listado de monitores (filtrado opcionalmente por la inicial del nombre)
        /// </summary>
        /// <param name="firstInitial">Filtro opcional para la inicial del monitor</param>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Instructor>> FindInstructors()
        {
            try
            {
                IEnumerable<Instructor> instructors = _repository.GetAll();

                return Ok(instructors);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }

        //Todo esto siguiente es por mi
        //Como hacer que se pueda dar de alta...
        /// <summary>
        /// POST /instructor
        /// Añade un instructor
        /// </summary>
        [HttpPost] //Cambiar esto de HttpGet a HttpPost para cambiar el tipo como aparece
        [ProducesResponseType(typeof(IEnumerable<Instructor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Instructor>> NewInstructors([FromBody] Instructor instructorNuevo) //Esto deberia hacer que se espere recibir los datos para un monitor, y como pedimos una clase aparecera para hacer una clase
        {
            try
            {
                //Obligar a que se pase un instructor
                if (instructorNuevo == null)
                {
                    return BadRequest(new ErrorResponse(1, "Tienes que dar un instructor no vacio"));
                }

                //Obligar a que haya un nombre, como me olvide de eso
                if (instructorNuevo.Name == null || instructorNuevo.Name.IsWhiteSpace() == true)
                {
                    return BadRequest(new ErrorResponse(2, "Tienes que poner un nombre"));
                }

                //Como hacemos que sea obligatorio el telefono en el API pero no en la base de datos?
                //Simplemente nada mas aparte de esto. El DarDeAlta solo necesita un instructor, y si se intenta hacer una clase instructor sin telefono lo combierte en string.Empty
                if (instructorNuevo.Phone == null || instructorNuevo.Phone.IsWhiteSpace() == true)
                {
                    return BadRequest(new ErrorResponse(3, "Tienes que poner un telefono"));
                }

                if (instructorNuevo.Email == null || instructorNuevo.Email.Contains("@yahoo.com") == false)
                {
                    return BadRequest(new ErrorResponse(3, "Eso no es un correo"));
                }
                if ((instructorNuevo.PhotoUrl == null) || ((instructorNuevo.PhotoUrl.Contains(".jpg") == false) && (instructorNuevo.PhotoUrl.Contains(".jpeg") == false) && (instructorNuevo.PhotoUrl.Contains(".png") == false) && (instructorNuevo.PhotoUrl.Contains(".gif") == false) && (instructorNuevo.PhotoUrl.Contains(".webp") == false)))
                {
                    return BadRequest(new ErrorResponse(3, "La imagen debe ser un .jpg, .jpeg, .png, .gif o .webp"));
                }
                IEnumerable<Instructor> instructors = _repository.GetAll();
                if (_repository.GetByName(instructorNuevo.Name.ToLower()) != null || _repository.GetByEmail(instructorNuevo.Email) != null)
                {
                    return BadRequest(new ErrorResponse(5, "Ya hay un monitor con ese nombre y correo"));
                    //Solo sale si tiene mismo nombre y correo
                }
                
                return Ok(instructors);
            }
            catch (Exception)
            {
                return BadRequest(new ErrorResponse(1, "Any problem in the Server"));
            }
        }
    }
}
