﻿namespace BackEnd.Domain.SeedWork;

public class Entity
{
    public Guid Id { get; protected set; }

    protected Entity() => Id = Guid.NewGuid();
}

