using SEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotUpdateStep : MonoSingleton<HotUpdateStep>, ILoadingStep
{

    public bool IsComplete { get; set; }

    public float Progress { get; set; }

    public override void Startup()
    {
        base.Startup();
    }

    protected override void Init()
    {
        base.Init();
        IsComplete = false;
    }

    public void Execute()
    {
        //StartCoroutine(ExecuteStep());
        OnComplete();
    }

    private IEnumerator ExecuteStep()
    {
        yield return null;
    }
    public void OnComplete()
    {
        Progress = 1;
        IsComplete = true;
        Log.Debug($"{name} OnComplete");
        DestroySelf();
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
