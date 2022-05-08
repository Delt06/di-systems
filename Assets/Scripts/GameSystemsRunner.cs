using DELTation.DIFramework.Systems;

public class GameSystemsRunner : SystemsRunnerBase
{
    protected override void ConstructSystems()
    {
        Add<HelloWorldSystem>();
        Add<TimeCounterSystem>();
    }
}