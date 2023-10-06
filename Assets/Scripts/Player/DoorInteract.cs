using System;
using UnityEngine;
using Zenject;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private GameObject _passCodePanel;
    
    [Inject] private InputManager _inputManager;
    [Inject] private RayFromEyes _rayFromEyes;

    private void Start()
    {
        _inputManager.OnInteractClicked += ShowCodePanel;
    }

    private void ShowCodePanel()
    {
        if (_rayFromEyes.hitObjectTag == "Door")
            _passCodePanel.SetActive(true);
    }
}
