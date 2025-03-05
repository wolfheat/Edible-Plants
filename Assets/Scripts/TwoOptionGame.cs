using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;
public class TwoOptionGame : BaseGame
{
    [SerializeField] private TextMeshProUGUI infoTextHeader;
    [SerializeField] private TextMeshProUGUI infoTextHeaderLatin;
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private GameObject infoObject;

    private string[] parts = { "Root", "Stem", "Leaf", "Flower", "Seed", "Fruit", "Avoid"};
    private string feral = "Förvildad";
    private string exchange = "Förväxlingsrisk";
    private string toxic = "Giftig";
    private void SetInfoText()
    {
        infoObject.SetActive(true);
        // Add edible in list
        infoTextHeader.text = activeQuestionData.ItemName;
        infoTextHeaderLatin.text = "(" + activeQuestionData.LatinName + ")"; // Italic
        infoText.text = activeQuestionData.info;
        
        infoText.text += "\n\n Edible Parts: ";

        int[] plantParts = activeQuestionData.PlantParts;

        for (int i = 0; i < plantParts.Length; i++) {
            int part = plantParts[i];
            if (part == 1) 
                if (i == 6) 
                    infoText.text += $"<color=#FF2222>{toxic} </color>"; // GIFTIG
                else
                    infoText.text += $"<color=#29A284>{parts[i]} </color>"; // EAT RAW
            else if (part == 2) {
                if (i == 6) 
                    infoText.text += $"<color=#985915>{exchange} </color>"; // FÖRVÄXLINGSRISK
                else
                    infoText.text += $"<color=#985915>{parts[i]} </color>"; // COOK 
            }
            else if (part == 3)
                infoText.text += $"<color=#C88E16>{parts[i]} </color>"; // COOK EXTENSIVELY / AVOID
            else if (part == 4)
                infoText.text += $"<color=#FF0000>{parts[i]} </color>"; // AVOID
        }        
        if(activeQuestionData.feral)
            infoText.text += $"<color=#985915>{feral}</color>";

    }

    public override void LoadRandomQuestion()
    {
        GetRandomQuestion();

        infoObject.SetActive(false);

        UpdateCurrentQuestionVisuals();

    }

    public override void AnswerOption(string answer)
    {
        base.AnswerOption(answer);
        if (infoText != null)
            SetInfoText();
    }

    protected override void UpdateCurrentQuestionVisuals()
    {
        int alternatives = 2;
        // Make correct amount of answer buttons
        CreateButtons(alternatives);

        resultText.text = "Make your Choice!";

        // Set Correct answer at random position and store the correct answer
        int correctIndex = Random.Range(0, alternatives);

        // Get list of X alternative answers from the Dictionary?
        List<string> alternativeAnswers = ItemDictionary.GetRandomWrongAnswerStrings(activeQuestionData.ItemName,alternatives-1);

        // Insert Correct answer into the list
        if(correctIndex<=alternativeAnswers.Count)
            alternativeAnswers.Insert(correctIndex, activeQuestionData.ItemName);
        else
            alternativeAnswers.Add(activeQuestionData.ItemName);

        // Place all button texts in the buttons
        for (int i = 0; i < alternativeAnswers.Count; i++) 
            buttons[i].UpdateText(alternativeAnswers[i]);

        // Set Sprite
        if (activeQuestionData.sprites.Length > 0) {
            QuestionImage.sprite = activeQuestionData.sprites[0];
            if(Random.Range(0,2)!=0)
                QuestionImage.rectTransform.localScale = new Vector3(QuestionImage.rectTransform.localScale.x*-1, 1,1); // FLIP
        }

        base.UpdateCurrentQuestionVisuals();
    }
    
    private void PrintAnswers(List<string> listItems, string prefix="") => Debug.Log(prefix+" [" + string.Join(',', listItems) + "]");

}
