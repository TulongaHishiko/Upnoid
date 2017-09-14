using Upnoid.Domain.Abstracts;

namespace Upnoid.Domain.Models
{
    public class Trailer : BaseModel
    {
        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
