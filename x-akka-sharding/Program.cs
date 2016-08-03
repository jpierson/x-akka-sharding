using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x_akka_sharding
{
    class Program
    {
        private static ActorSystem system;
        private static IActorRef gateway;
        private static Random randomNumberGenerator;

        static void Main(string[] args)
        {
            system = ActorSystem.Create("test");
            randomNumberGenerator = new Random();

            var props = Props
                .Create<GateWayActor>();
            gateway = system.ActorOf(props, "gateway");

            foreach (var i in Enumerable.Range(0, 100000))
            {
                new DomainEvent(i, randomNumberGenerator.Next(1, 10));
            }

            system.WhenTerminated.Wait();
        }
    }
}
