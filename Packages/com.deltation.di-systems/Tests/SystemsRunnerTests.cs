using NUnit.Framework;

namespace DELTation.DIFramework.Systems.Tests
{
    [TestFixture]
    internal class SystemsRunnerTests
    {
        [Test]
        public void GivenInitSystem_WhenCallingOnStarted_ThenSystemCalledOnce()
        {
            // Arrange
            var system = new TestInitSystem();
            var systemsRunner = new ConfigurableSystemsRunner(system);

            // Act
            systemsRunner.OnStart();

            // Assert
            Assert.AreEqual(1, system.CalledTimes);
        }

        [Test]
        public void GivenDestroySystem_WhenCallingOnDestroyed_ThenSystemCalledOnce()
        {
            // Arrange
            var system = new TestDestroySystem();
            var systemsRunner = new ConfigurableSystemsRunner(system);

            // Act
            systemsRunner.OnStart();
            systemsRunner.OnDestroy();

            // Assert
            Assert.AreEqual(1, system.CalledTimes);
        }

        [Test]
        public void GivenRunSystem_WhenCallingOnUpdate_ThenSystemCalledOnce()
        {
            // Arrange
            var system = new TestRunSystem();
            var systemsRunner = new ConfigurableSystemsRunner(system);

            // Act
            systemsRunner.OnStart();
            systemsRunner.OnUpdate();

            // Assert
            Assert.AreEqual(1, system.CalledTimes);
        }

        [Test]
        public void GivenLateRunSystem_WhenCallingOnLateUpdate_ThenSystemCalledOnce()
        {
            // Arrange
            var system = new TestLateRunSystem();
            var systemsRunner = new ConfigurableSystemsRunner(system);

            // Act
            systemsRunner.OnStart();
            systemsRunner.OnLateUpdate();

            // Assert
            Assert.AreEqual(1, system.CalledTimes);
        }

        [Test]
        public void GivenPhysicsRunSystem_WhenCallingOnFixedUpdate_ThenSystemCalledOnce()
        {
            // Arrange
            var system = new TestPhysicsRunSystem();
            var systemsRunner = new ConfigurableSystemsRunner(system);

            // Act
            systemsRunner.OnStart();
            systemsRunner.OnFixedUpdate();

            // Assert
            Assert.AreEqual(1, system.CalledTimes);
        }

        [Test]
        public void GivenRunSystem_WhenCallingOnUpdateWithoutOnStart_ThenSystemIsNotCalled()
        {
            // Arrange
            var system = new TestRunSystem();
            var systemsRunner = new ConfigurableSystemsRunner(system);

            // Act
            systemsRunner.OnUpdate();

            // Assert
            Assert.AreEqual(0, system.CalledTimes);
        }

        [Test, TestCase(0), TestCase(1), TestCase(2), TestCase(5)]
        public void GivenManySystems_WhenCallingAllCallbacks_ThenAllSystemsAreCalled(int iterations)
        {
            // Arrange
            var systems = new ISystem[]
            {
                new TestDestroySystem(),
                new TestRunSystem(),
                new TestLateRunSystem(),
                new TestPhysicsRunSystem(),
            };
            var systemsRunner = new ConfigurableSystemsRunner(systems);
            systemsRunner.OnStart();

            // Act
            for (var i = 0; i < iterations; i++)
            {
                systemsRunner.OnUpdate();
                systemsRunner.OnLateUpdate();
                systemsRunner.OnFixedUpdate();
                systemsRunner.OnDestroy();
            }

            // Assert
            foreach (var system in systems)
            {
                var testSystem = (TestSystemBase) system;
                Assert.AreEqual(iterations, testSystem.CalledTimes);
            }
        }

        [Test, TestCase(1), TestCase(2), TestCase(5)]
        public void GivenInitSystem_WhenCallingOnStartMultipleTimes_ThenCalledTimesIsCorrect(int startCalls)
        {
            // Arrange
            var system = new TestInitSystem();
            var systemsRunner = new ConfigurableSystemsRunner(system);

            // Act
            for (var i = 0; i < startCalls; i++)
            {
                systemsRunner.OnStart();
            }

            // Assert
            Assert.AreEqual(startCalls, system.CalledTimes);
        }
    }
}