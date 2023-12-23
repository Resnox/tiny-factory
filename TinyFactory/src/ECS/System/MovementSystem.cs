﻿using System;
using System.Runtime.CompilerServices;
using Arch.Core;
using TinyFactory.Component;
using TinyFactory.ECS;

namespace TinyFactory.System;

public class MovementSystem : BaseSystem
{
    private readonly QueryDescription queryDescription = new QueryDescription().WithAll<Position>();

    public MovementSystem(World world) : base(world)
    {
        
    }

    public override void Update(in double deltaTime)
    {
        base.Update(in deltaTime);

        var movementJob = new MoveJob(deltaTime);
        world.InlineQuery<MoveJob, Position>(in queryDescription, ref movementJob);
    }

    private readonly struct MoveJob : IForEach<Position>
    {
        private readonly double deltaTime;

        public MoveJob(double deltaTime)
        {
            this.deltaTime = deltaTime;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position position)
        {
            position.X += 500 * deltaTime;
            position.Y += 500 * deltaTime;
        }
    }
}