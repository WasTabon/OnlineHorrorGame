using UnityEngine;
using TMPro;

public class CodePassword : MonoBehaviour
{
    [SerializeField] private int _password;

    [SerializeField] private TextMeshProUGUI _textCellOne;
    [SerializeField] private TextMeshProUGUI _textCellTwo;
    [SerializeField] private TextMeshProUGUI _textCellThree;
    [SerializeField] private TextMeshProUGUI _textCellFour;

    public int _currentTextCell = 1;
    
    public void CellsController(string textToInput)
    {
        switch (_currentTextCell)
        {
            case 1:
                SetText(_textCellOne, textToInput);
                break;
            case 2:
                SetText(_textCellTwo, textToInput);
                break;
            case 3:
                SetText(_textCellThree, textToInput);
                break;
            case 4:
                SetText(_textCellFour, textToInput);
                _currentTextCell = 1;
                break;
        }
    }

    private void SetText(TextMeshProUGUI cell ,string textToInput)
    {
        cell.text = textToInput;
        _currentTextCell++;
    }
}