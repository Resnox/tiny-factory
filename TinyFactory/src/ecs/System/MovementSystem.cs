using System.Runtime.CompilerServices;
using Arch.Core;
using TinyFactory.ECS.Component;
using TinyFactory.Engine.update;

namespace TinyFactory.ECS.System;

public class MovementSystem : AbstractSystem, IUpdatable
{
    private readonly QueryDescription queryDescription = new QueryDescription().WithAll<Position>();

    public MovementSystem(World world) : base(world)
    {
    }

    public void Update(in float deltaTime)
    {
        var movementJob = new MoveJob(deltaTime);
        World.InlineQuery<MoveJob, Position>(in queryDescription, ref movementJob);
    }

    private readonly struct MoveJob : IForEach<Position>
    {
        private readonly float deltaTime;

        public MoveJob(float deltaTime)
        {
            this.deltaTime = deltaTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(ref Position position)
        {
        }
    }
}