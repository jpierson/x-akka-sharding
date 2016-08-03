using Akka.Actor;
using System;

namespace x_akka_sharding
{
    internal class AggregateActor : ReceiveActor
    {
        public AggregateActor()
        {
            Console.WriteLine($"Aggregate actor {Self.Path.Name} constructed");
        }
    }
}