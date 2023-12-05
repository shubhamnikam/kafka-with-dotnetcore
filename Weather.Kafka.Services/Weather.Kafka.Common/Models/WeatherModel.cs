using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Kafka.Common.Models
{
    public class WeatherModel
    {
        public string Id { get; set; }
        public string City { get; set; }
        public float Temperature { get; set; }
        public DateTimeOffset CreatedTimestamp { get; set; }
    }
}
