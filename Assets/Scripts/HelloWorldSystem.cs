using DELTation.DIFramework.Systems;
using UnityEngine;

public class HelloWorldSystem : IInitSystem
{
    public void Init()
    {
        Debug.Log("Hello DI Systems!");
    }
}