namespace DELTation.DIFramework.Systems.Tests
{
    internal class ConfigurableSystemsRunner : SystemsRunnerBase
    {
        private readonly ISystem[] _systems;

        public ConfigurableSystemsRunner(params ISystem[] systems) => _systems = systems;

        protected override void ConstructSystems()
        {
            foreach (var system in _systems)
            {
                Add(system);
            }
        }
    }
}