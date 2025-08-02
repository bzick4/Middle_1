using UnityEngine;
using Zenject;

public class MyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("INJECT");
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
        Debug.Log("Test1");
    }
}

public interface ITest
{
    void Echo();
}