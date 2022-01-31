using Reactive.Bindings;
using Snow.View;
using System;

namespace Snow
{
    class SimulationModel : ISimulationModel
    {
        public SimulationModel(Scene inScene)
        {
            Scene = inScene;
        }

        public void Initialize()
            => Initialize((float)(new Random().NextDouble()));
        public void Initialize(float inSeed)
            => Scene.SnowCover.Randomize(inSeed);

        public void TimeStep(float inTimeStep)
            => Scene.TimeStep(TimeStepScale.Value * inTimeStep);

        public IReactiveProperty<float> TimeStepScale
        { get; } = new ReactiveProperty<float>(1.0f);

        public readonly Scene Scene;
    }
}
