using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseGame : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI resultText;
    [SerializeField] protected Image QuestionImage;
    [SerializeField] protected GameObject buttonHolder;
    [SerializeField] protected ButtonOption buttonPrefab;
    [SerializeField] protected ButtonOption[] buttons;

    [Header("Info")]
    [SerializeField] protected TextMeshProUGUI infoTextHeader;
    [SerializeField] protected TextMeshProUGUI infoTextHeaderLatin;
    [SerializeField] protected TextMeshProUGUI infoText;
    [SerializeField] protected GameObject infoObject;


    protected QuestionData activeQuestionData = null;


    private string[] parts = { "Root", "Stem", "Leaf", "Flower", "Seed", "Fruit", "Avoid" };
    private string feral = "Förvildad";
    private string exchange = "Förväxlingsrisk";
    private string toxic = "Giftig";
    private string protectText = "Fridlyst!";
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
        if (activeQuestionData.feral)
            infoText.text += $"<color=#985915>{feral}</color>";
        if (activeQuestionData.protectedPlant)
            infoText.text += $"<color=#FF2222>{protectText}</color>";

    }
    protected virtual void UpdateCurrentQuestionVisuals()
    {
        Debug.Log("Update Question Visuals has data = "+(activeQuestionData != null));  
        // Set Sprite
        if (activeQuestionData.sprites.Length > 0)
            QuestionImage.sprite = activeQuestionData.sprites[0];
    }



    private void OnEnable()
    {
        LoadRandomQuestion();
    }

    public abstract void LoadRandomQuestion();

    public virtual void AnswerOption(string answer)
    {
        // Make Correct Green and Wrong Red
        resultText.text = (answer == activeQuestionData.ItemName) ? $"<color=#44FF44>Correct!</color>" : $"<color=#FF4444>Wrong!</color>";
        if (infoText != null)
            SetInfoText();
    }


    protected void GetRandomQuestion()
    {
        Debug.Log("Trying to get Random Question");
        QuestionData questionData = ItemDictionary.GetRandomQuestionData();
        if (questionData == null) {
            Debug.Log("Could not load any Question!");
            return;
        }

        activeQuestionData = questionData;
        infoObject.SetActive(false);
    }

    protected void CreateButtons(int alternatives)
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

}
