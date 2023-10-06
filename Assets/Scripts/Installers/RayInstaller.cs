using UnityEngine;
using Zenject;

public class RayInstaller : MonoInstaller
{
    [SerializeField] private RayFromEyes _rayFromEyes;

    public override void InstallBindings()
    {
        Container.Bind<RayFromEyes>().FromInstance(_rayFromEyes).AsSingle();
    }
}
