namespace DELTation.DIFramework.Systems.Tests
{
    internal class TestRunSystem : TestSystemBase, IRunSystem
    {
        public void Run()
        {
            OnCalled();
        }
    }
}