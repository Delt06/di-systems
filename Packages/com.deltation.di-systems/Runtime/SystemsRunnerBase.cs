using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            Add(system);
        }

        protected void Add([NotNull] ISystem system)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));

            TryAddSystem(_initSystems, system);
            TryAddSystem(_runSystems, system);
            TryAddSystem(_destroySystems, system);
            TryAddSystem(_lateRunSystems, system);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void TryAddSystem<T>(ICollection<T> systems, ISystem system) where T : class, ISystem
        {
            if (system is T castedSystem)
                systems.Add(castedSystem);
        }
    }
}