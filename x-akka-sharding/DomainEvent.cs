using System;

namespace x_akka_sharding
{
    internal class DomainEvent
    {
        public int Id { get; }

        public int Tenant { get; }
        public DomainEvent(int id, int tenant)
        {
            this.Id = id;
            this.Tenant = tenant;
        }
    }
}