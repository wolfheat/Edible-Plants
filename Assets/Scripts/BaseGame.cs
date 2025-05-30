﻿using TMPro;
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
    [SerializeField] protected GameObject armyObject;
    [SerializeField] protected TextMeshProUGUI armyText;

    public bool HaveInputtedAnswer { get; set; }


    protected QuestionData activeQuestionData = null;

    private string[] parts = { "Rot", "Stjälk", "Blad", "Blommor", "Frö", "Frukt", "Undvik", "Lav/Svamp"};
    //private string[] parts = { "Root", "Stem", "Leaf", "Flower", "Seed", "Fruit", "Avoid" };
    private string feral = "Förvildad";
    private string exchange = "Förväxlingsrisk";
    private string toxic = "Giftig";
    private string protectText = "Fridlyst!";
    private string sapText = "Sav";
    private void SetInfoText()
    {
        if(infoObject != null)
            infoObject.SetActive(true);
        // Add edible in list
        infoTextHeader.text = activeQuestionData.ItemName;
        infoTextHeaderLatin.text = "(" + activeQuestionData.LatinName + ")"; // Italic
        infoText.text = activeQuestionData.info;

        infoText.text += "\n\nÄtliga Delar: ";
        //infoText.text += "\n\n Edible Parts: ";

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
            else if (part == 7)
                infoText.text += $"<color=#FF0000>{parts[i]} </color>"; // AVOID
        }
        if (activeQuestionData.feral)
            infoText.text += $"<color=#985915>{feral}</color>";
        if (activeQuestionData.protectedPlant)
            infoText.text += $"<color=#FF2222>{protectText}</color>";
        if (activeQuestionData.sap > 0)
            infoText.text += $"<color=#FF2222>{sapText}</color>";

        // Army Info
        if(activeQuestionData.army != 0) {
            armyObject.SetActive(true);
            switch (activeQuestionData.army) {
                case 1:
                    armyText.text = "TOP 15";
                    break;
                    case 2:
                    armyText.text = "TOP 50";
                    break;
                    default:
                    armyText.text = "-----";
                    break;
            }
        }else armyObject.SetActive(false);


    }
    protected virtual void UpdateCurrentQuestionVisuals()
    {
        Debug.Log("Update Question Visuals has data = "+(activeQuestionData != null));  
        // Set Sprite
        if (activeQuestionData.sprites.Length > 0)
            QuestionImage.sprite = activeQuestionData.sprites[0];
    }

    protected void SetSprite()
    {
        if (activeQuestionData.sprites.Length > 0) {
            QuestionImage.sprite = activeQuestionData.sprites[Random.Range(0, activeQuestionData.sprites.Length)];
            if (Random.Range(0, 2) != 0)
                QuestionImage.rectTransform.localScale = new Vector3(QuestionImage.rectTransform.localScale.x * -1, 1, 1); // FLIP
        }
    }


    protected virtual void OnEnable()
    {
        LoadRandomQuestion();
    }

    public virtual void LoadRandomQuestion() => HaveInputtedAnswer = false;

    public virtual bool AnswerOption(string answer)
    {
        HaveInputtedAnswer = true;
        bool correctAnswer = answer == activeQuestionData.ItemName;

        // Make Correct Green and Wrong Red
        resultText.text = correctAnswer ? "Correct!" : "Wrong";
        resultText.color = correctAnswer ? Settings.Instance.CorrectColor : Settings.Instance.WrongColor; 

        if (infoText != null)
            SetInfoText();
        return correctAnswer;
    }


    protected void GetQuestion(int step = 0)
    {
        Debug.Log("Trying to get Question");
        QuestionData questionData = ItemDictionary.GetQuestionData(step);
        if (questionData == null) {
            Debug.Log("Could not load any Question!");
            return;
        }
        activeQuestionData = questionData;
        SetInfoText();
        // Set Sprite
        SetSprite();
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
