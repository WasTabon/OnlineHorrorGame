using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textInteract;
    
    [Inject] private RayFromEyes _rayFromEyes;

    private void Update()
    {
        ControlInteractText("Door");
    }

    private void ControlInteractText(string interactWith)
    {
        if (_rayFromEyes.hitObjectTag == interactWith)
        {
            _textInteract.gameObject.SetActive(true);
            _textInteract.text = $"Press [E] to interact with {interactWith}";
        }
        else
        {
            _textInteract.gameObject.SetActive(false);
        }
    }
}
