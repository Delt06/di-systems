namespace DELTation.DIFramework.Systems.Tests
{
    internal class TestInitSystem : TestSystemBase, IInitSystem
    {
        public void Init()
        {
            OnCalled();
        }
    }
}