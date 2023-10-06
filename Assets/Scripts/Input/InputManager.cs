using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private int _cameraButton;
    public Action OnCameraClicked;
    
    [SerializeField] private KeyCode _interactButton;
    public Action OnInteractClicked;

    public ObservableVariable<float> ScrollDirection { get; private set; }

    private float _scrollDirection;

    private void Awake()
    {
        ScrollDirection = new ObservableVariable<float>(0);
    }

    private void Update()
    {
        CheckMouseClick(_cameraButton, OnCameraClicked);
        CheckScrollMouse();
        CheckClick(_interactButton, OnInteractClicked);
    }

    private void CheckClick(KeyCode keyCode, Action action)
    {
        if (Input.GetKeyDown(keyCode))
            action?.Invoke();
    }
    private void CheckMouseClick(int keyCode, Action action)
    {
        if (Input.GetMouseButtonDown(keyCode))
            action?.Invoke();
    }
    private void CheckScrollMouse()
    {
        _scrollDirection = Input.mouseScrollDelta.y;
        ScrollDirection.Value = _scrollDirection;
    }
}
