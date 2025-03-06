using System.Collections.Generic;
using UnityEngine;

public class KeyboardGame : BaseGame
{

    [Header("Virtual Keyboard")]
    [SerializeField] protected GameObject keyboard;

    public override void LoadRandomQuestion()
    {
        GetRandomQuestion();

        keyboard.SetActive(true);

        // Reset input

        UpdateCurrentQuestionVisuals();

    }

    public override void AnswerOption(string answer)
    {
        base.AnswerOption(answer);

        // Remove the keyboard here
        keyboard.SetActive(false);
    }

    private void CreateBestButton(int alternatives = 1)
    {
        for (int i = buttons.Length - 1; i >= 0; i--) {
            Destroy(buttons[i].gameObject);
        }

        buttons = new ButtonOption[alternatives];
        for (int i = 0; i < alternatives; i++) {
            ButtonOption option = Instantiate(buttonPrefab, buttonHolder.transform);
            option.SetGame(this);
            buttons[i] = option;
        }
    }

    internal void EnterTypeIn(string input)
    {
        Debug.Log("Handle player input " + input);

        // Update the button to show the first valid name it finds
        UpdateButtons(input);
        //UpdateCurrentQuestionVisuals();
    }
    private void UpdateButtons(string input)
    {
        List<string> buttonNames = ItemDictionary.GetBestFit(input,2);

        Debug.Log("Recieved "+buttonNames.Count+" buttons for the entrence "+input);

        // Make correct amount of answer buttons
        CreateButtons(buttonNames.Count);

        // Place all button texts in the buttons
        for (int i = 0; i < buttonNames.Count; i++)
            buttons[i].UpdateText(buttonNames[i]);
        

    }

    protected override void UpdateCurrentQuestionVisuals()
    {
        UpdateButtons(""); // Resets button when new question loads
        resultText.text = "Make your Choice!";

        base.UpdateCurrentQuestionVisuals();
    }

}
