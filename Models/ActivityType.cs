using System.Text.Json.Serialization;

namespace _4VGym_pero_con_BBDD_relacional.Models
{
    public class ActivityType
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int NumberMonitors { get; set; } = 1;

        //Propiedad de navegacion
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}
