using System.Collections.Generic;
using DELTation.DIFramework.Lifecycle;
using JetBrains.Annotations;

namespace DELTation.DIFramework.Systems
{
    public abstract class SystemsRunnerBase : IStartable, IUpdatable, IDestroyable, ILateUpdatable
    {
        private readonly List<IDestroySystem> _destroySystems = new List<IDestroySystem>();
        private readonly List<IInitSystem> _initSystems = new List<IInitSystem>();
        private readonly List<ILateRunSystem> _lateRunSystems = new List<ILateRunSystem>();
        private readonly List<IRunSystem> _runSystems = new List<IRunSystem>();

        public void OnDestroy()
        {
            foreach (var destroySystem in _destroySystems)
            {
                destroySystem.Destroy();
            }
        }

        public void OnLateUpdate()
        {
            foreach (var lateRunSystem in _lateRunSystems)
            {
                lateRunSystem.LateRun();
            }
        }

        public void OnStart()
        {
            ConstructSystems();

            foreach (var initSystem in _initSystems)
            {
                initSystem.Init();
            }
        }

        public void OnUpdate()
        {
            foreach (var runSystem in _runSystems)
            {
                runSystem.Run();
            }
        }

        protected abstract void ConstructSystems();

        protected void Add<[MeansImplicitUse] T>() where T : class, ISystem
        {
            var system = Di.Create<T>();
            // ReSharper disable ConvertIfStatementToSwitchStatement
            if (system is IInitSystem initSystem)
                _initSystems.Add(initSystem);
            if (system is IRunSystem runSystem)
                _runSystems.Add(runSystem);
            if (system is IDestroySystem destroySystem)
                _destroySystems.Add(destroySystem);
            if (system is ILateRunSystem lateRunSystem)
                _lateRunSystems.Add(lateRunSystem);
            // ReSharper restore ConvertIfStatementToSwitchStatement
        }
    }
}