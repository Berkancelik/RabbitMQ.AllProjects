using MassTransit;
using RabbitMQ.ESB.MassTransit.Shared.ReequestResponseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.ESB.massTransitRequestresponsePattern.Consumer.Consumer
{
    internal class RequestMessageConsumer:IConsumer<RequestMessage>
    {
    }
}
