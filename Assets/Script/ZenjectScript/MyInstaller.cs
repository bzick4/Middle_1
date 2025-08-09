using System.ComponentModel;
using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{

    [SerializeField] private PlayerStats _PlayerStats;
    [SerializeField] private bool _IsDummyStats;

    public override void InstallBindings()
    {
        //IPlayerStats stats = _IsDummyStats ? new PlayerStatsDummy() : _PlayerStats;
       // Container.Bind<IPlayerStats>().FromInstance(stats).AsSingle().NonLazy();

        // Container.Bind<string>().FromInstance("INJECT");
        Container.Bind<GreetMe>().AsSingle().NonLazy();
        Container.Bind<ITest>().To<Test1>().AsSingle().NonLazy();
    }
}

public class GreetMe
{
    public GreetMe(string test)
    {
        Debug.Log(test);
    }
}

public class Test1 : ITest
{
    public void Echo()
    {
        Debug.Log("aaaa");
    }
}

public interface ITest
{
    void Echo();
}