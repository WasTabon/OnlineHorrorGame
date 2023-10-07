using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Zenject;

public class CodeNote : MonoBehaviour
{
    [SerializeField] private GameObject _codeNote;
    
    [Inject] private InputManager _inputManager;
    [Inject] private RayFromEyes _rayFromEyes;

    private void Start()
    {
        _inputManager.OnInteractClicked += ShowCodeNote;
    }

    private void ShowCodeNote()
    {
        if (_rayFromEyes.hitObjectTag == "Note")
        {
            _codeNote.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
