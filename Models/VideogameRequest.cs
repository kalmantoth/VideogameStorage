using Microsoft.AspNetCore.Mvc;

namespace VideogameStorage.Models
{
    public class VideogameRequest
    {
        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 15;
        
        [FromQuery(Name = "offset")]
        public int Offset { get; set; }
    }
}
