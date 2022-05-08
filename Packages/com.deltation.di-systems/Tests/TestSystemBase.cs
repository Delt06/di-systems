namespace DELTation.DIFramework.Systems.Tests
{
    internal class TestSystemBase : ISystem
    {
        public int CalledTimes;

        protected void OnCalled()
        {
            CalledTimes++;
        }
    }
}