using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class TwoOptionGame : BaseGame
{
    public override void LoadRandomQuestion()
    {
        base.LoadRandomQuestion();
        GetRandomQuestion();

        infoObject.SetActive(false);

        UpdateCurrentQuestionVisuals();
    }

    protected override void UpdateCurrentQuestionVisuals()
    {
        int alternatives = 2;
        // Make correct amount of answer buttons
        CreateButtons(alternatives);

        resultText.text = "Make your Choice!";
        resultText.color = Settings.Instance.NeutralGreyColor;

        // Set Correct answer at random position and store the correct answer
        int correctIndex = Random.Range(0, alternatives);

        // Get list of X alternative answers from the Dictionary?
        List<string> alternativeAnswers = ItemDictionary.GetRandomWrongAnswerStrings(activeQuestionData.ItemName, alternatives - 1);

        // Insert Correct answer into the list
        if (correctIndex <= alternativeAnswers.Count)
            alternativeAnswers.Insert(correctIndex, activeQuestionData.ItemName);
        else
            alternativeAnswers.Add(activeQuestionData.ItemName);

        // Place all button texts in the buttons
        for (int i = 0; i < alternativeAnswers.Count; i++)
            buttons[i].UpdateText(alternativeAnswers[i]);

        // Set Sprite
        SetSprite();

        base.UpdateCurrentQuestionVisuals();
    }

    
    private void PrintAnswers(List<string> listItems, string prefix="") => Debug.Log(prefix+" [" + string.Join(',', listItems) + "]");

}
