namespace DELTation.DIFramework.Systems.Tests
{
    internal class TestLateRunSystem : TestSystemBase, ILateRunSystem
    {
        public void LateRun()
        {
            OnCalled();
        }
    }
}