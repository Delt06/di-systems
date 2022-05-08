using DELTation.DIFramework;
using DELTation.DIFramework.Containers;

public class DiCompositionRoot : DependencyContainerBase
{
    protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
    {
        builder.Register<GameSystemsRunner>();
    }
}