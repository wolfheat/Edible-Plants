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

    public void OnButtonClicked(Button button)
    {
        Debug.Log("Clicked "+button.name);

        // Update the inputed text
        if (button.name == "Back") {
            if (input.Length > 0) {
                input = input.Substring(0, input.Length - 1);
            }
        }
        else {
            input += button.name;
        }

        inputText.text = input;

        if (game != null)
            game.EnterTypeIn(input);
    }


}
