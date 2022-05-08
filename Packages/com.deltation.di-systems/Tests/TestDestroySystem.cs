namespace DELTation.DIFramework.Systems.Tests
{
    internal class TestDestroySystem : TestSystemBase, IDestroySystem
    {
        public void Destroy()
        {
            OnCalled();
        }
    }
}