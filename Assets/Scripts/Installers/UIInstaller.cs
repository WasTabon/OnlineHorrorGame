using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIManager _uiManager;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
    }
}
