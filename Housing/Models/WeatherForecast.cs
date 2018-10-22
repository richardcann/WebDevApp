using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Housing.Models
{
    public class WeatherForecast
    {
        [Key]
        public string DateFormatted { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }

        public int TemperatureF
        {
            get
            {
                return 32 + (int)(TemperatureC / 0.5556);
            }
        }
    }
}
