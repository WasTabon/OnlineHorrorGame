using UnityEngine;
using TMPro;

public class CodePassword : MonoBehaviour
{
    [SerializeField] private int _password;

    [SerializeField] private TextMeshProUGUI _textCellOne;
    [SerializeField] private TextMeshProUGUI _textCellTwo;
    [SerializeField] private TextMeshProUGUI _textCellThree;
    [SerializeField] private TextMeshProUGUI _textCellFour;

    private int _currentTextCell = 1;
    
    public void CellsController(string textToInput)
    {
        switch (_currentTextCell)
        {
            case 1:
                SetText(textToInput);
                break;
            case 2:
                SetText(textToInput);
                break;
            case 3:
                SetText(textToInput);
                break;
            case 4:
                SetText(textToInput);
                _currentTextCell = 1;
                break;
        }
    }

    private void SetText(string textToInput)
    {
        _textCellOne.text = textToInput;
        _currentTextCell++;
    }
}