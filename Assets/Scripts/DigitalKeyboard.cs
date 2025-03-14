using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DigitalKeyboard : MonoBehaviour
{
    [SerializeField] Button[] Keys;
    [SerializeField] KeyboardGame game;
    [SerializeField] TextMeshProUGUI inputText;

    private string input = "";

    public void ResetInput()
    {
        input = "";
        inputText.text = input;
    }

    public void OnButtonClicked(string buttonString)
    {
        Debug.Log("Clicked "+buttonString);

        // Update the inputed text
        if (buttonString.Length > 1) { // BACK
            if (input.Length > 0) {
                input = input.Substring(0, input.Length - 1);
            }
        }
        else {
            input += buttonString;
        }

        inputText.text = input;

        if (game != null)
            game.EnterTypeIn(input);
    }


}
