using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] private GameObject _passCodePanel;
    
    [Inject] private InputManager _inputManager;
    [Inject] private RayFromEyes _rayFromEyes;

    private FirstPersonController _firstPersonController;

    private void Start()
    {
        _firstPersonController = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        _inputManager.OnInteractClicked += ShowCodePanel;
    }

    private void ShowCodePanel()
    {
        if (_rayFromEyes.hitObjectTag == "Door")
        {
            _firstPersonController.m_MouseLook.SetCursorLock(false);
            _passCodePanel.SetActive(true);
        }
    }
}
