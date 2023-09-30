using System;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [Inject] private InputManager _inputManager;

    private Animator _animator;

    private void Start()
    {
        _inputManager.OnCameraClicked += TriggerCameraAnim;
        _animator = GetComponent<Animator>();
    }

    private void TriggerCameraAnim()
    {
        _animator.SetTrigger("CameraButtonClicked");
    }
}
