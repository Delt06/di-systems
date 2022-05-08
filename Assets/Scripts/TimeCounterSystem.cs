using DELTation.DIFramework.Systems;
using UnityEngine;

public class TimeCounterSystem : IRunSystem
{
    private float _timer;

    public void Run()
    {
        _timer += Time.deltaTime;
        Debug.Log(_timer.ToString("F2"));
    }
}