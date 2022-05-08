using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DELTation.DIFramework.Lifecycle;
using JetBrains.Annotations;

namespace DELTation.DIFramework.Systems
{
    public abstract class SystemsRunnerBase : IStartable, IUpdatable, IDestroyable, ILateUpdatable, IFixedUpdatable
    {
        private readonly List<IDestroySystem> _destroySystems = new List<IDestroySystem>();
        private readonly List<IInitSystem> _initSystems = new List<IInitSystem>();
        private readonly List<ILateRunSystem> _lateRunSystems = new List<ILateRunSystem>();
        private readonly List<IPhysicsRunSystem> _physicsRunSystems = new List<IPhysicsRunSystem>();
        private readonly List<IRunSystem> _runSystems = new List<IRunSystem>();

        public void OnDestroy()
        {
            foreach (var destroySystem in _destroySystems)
            {
                destroySystem.Destroy();
            }
        }

        public void OnFixedUpdate()
        {
            foreach (var physicsRunSystem in _physicsRunSystems)
            {
                physicsRunSystem.PhysicsRun();
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
            Cleanup();
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

        private void Cleanup()
        {
            _destroySystems.Clear();
            _initSystems.Clear();
            _lateRunSystems.Clear();
            _runSystems.Clear();
            _physicsRunSystems.Clear();
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
            TryAddSystem(_physicsRunSystems, system);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void TryAddSystem<T>(ICollection<T> systems, ISystem system) where T : class, ISystem
        {
            if (system is T castedSystem)
                systems.Add(castedSystem);
        }
    }
}