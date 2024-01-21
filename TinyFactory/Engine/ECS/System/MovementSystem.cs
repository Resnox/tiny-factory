using System.Runtime.CompilerServices;
using Arch.Core;
using TinyFactory.Engine.Core.Interfaces;
using TinyFactory.Engine.ECS.Component;

namespace TinyFactory.Engine.ECS.System;

public class MovementSystem : AbstractSystem, IUpdatable
{
    private readonly QueryDescription queryDescription = new QueryDescription().WithAll<Position>();

    public MovementSystem(World world) : base(world)
    {
    }

    #region IUpdatable Members

    public void Update(in float deltaTime)
    {
        var movementJob = new MoveJob(deltaTime);
        World.InlineQuery<MoveJob, Position>(in queryDescription, ref movementJob);
    }

    #endregion

    #region Nested type: MoveJob

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

    #endregion
}