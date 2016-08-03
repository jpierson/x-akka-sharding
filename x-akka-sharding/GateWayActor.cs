using Akka.Actor;
using Akka.Cluster.Sharding;
using Akka.Routing;

namespace x_akka_sharding
{
    internal class GateWayActor : ReceiveActor
    {
        private IActorRef region;

        public GateWayActor()
        {
            ConfigureSharding();

            Receive<DomainEvent>(e => region.Tell(new Envelope(e.Tenant.ToString(), e.Id.ToString(), e)));
        }

        private void ConfigureSharding()
        {
            var system = Context.System;

            // register actor type as a sharded entity
            region = ClusterSharding.Get(system).Start(
                typeName: "aggregate",
                entityProps: Props.Create<AggregateActor>(),
                settings: ClusterShardingSettings.Create(system),
                messageExtractor: new MessageExtractor());
        }


        // define envelope used to message routing
        public sealed class Envelope
        {
            public readonly string ShardId;
            public readonly string EntityId;
            public readonly object Message;

            public Envelope(string shardId, string entityId, object message)
            {
                ShardId = shardId;
                EntityId = entityId;
                Message = message;
            }
        }

        // define, how shard id, entity id and message itself should be resolved
        public sealed class MessageExtractor : IMessageExtractor
        {
            public string EntityId(object message)
            {
                return (message as Envelope)?.EntityId.ToString();
            }

            public string ShardId(object message)
            {
                return (message as Envelope)?.ShardId.ToString();
            }

            public object EntityMessage(object message)
            {
                return (message as Envelope)?.Message;
            }
        }

    }
}