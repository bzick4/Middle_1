using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{

    [Header("Переключение")]
    [SerializeField] private bool _IsConfig = true;

    [SerializeField] private Settings _HealthSetting;

    public override void InstallBindings()
    {
        if (_IsConfig)
        {
            Container.Bind<IHealthConfig>().FromInstance(_HealthSetting).AsSingle();
        }
        else
        {
            Container.Bind<IHealthConfig>().To<DummyHealth>().AsSingle();
        }

        Container.Bind<IHealthService>().To<HealthService>().AsSingle().NonLazy();


    }
}

public interface IHealthService
{
}

public class HealthService : IHealthService
{
    private IHealthConfig _config;
    private float _currentHealth;

    public HealthService(IHealthConfig config)
    {
        _config = config;
        _currentHealth = _config.HeroHealth;
        Debug.Log($"Здоровье Равно {_config.HeroHealth}");
    }

}