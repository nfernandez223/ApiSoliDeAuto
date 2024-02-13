using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RabbitMQConfig
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
    }
}
