using System.Text.Json.Serialization;

namespace _4VGym_pero_con_BBDD_relacional.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhotoUrl { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public ICollection<Activity> Activitys { get; set; } = new List<Activity>();
    }
}
