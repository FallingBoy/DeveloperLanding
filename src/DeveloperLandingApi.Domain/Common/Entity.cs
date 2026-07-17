using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Domain.Common
{
    // Наш базовый энтити, который будет использоваться для всех сущностей в нашей системе. Он содержит уникальный идентификатор (Id) и методы для сравнения сущностей.
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        protected Entity(Guid id)
        {
            Id = id;
        }
        public override bool Equals(object? obj)
        {
            if (obj is not Entity entity)
                return false;


            if (ReferenceEquals(this, entity))
                return true;


            return Id == entity.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
