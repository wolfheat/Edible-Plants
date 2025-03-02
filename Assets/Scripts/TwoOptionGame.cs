using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TwoOptionGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private Image QuestionImage;
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private GameObject buttonHolder;
    [SerializeField] private ButtonOption buttonPrefab;
    [SerializeField] private ButtonOption[] buttons;

    private QuestionData activeQuestionData = null;
    private int correctIndex = -1;

    private void OnEnable()
    {
        LoadRandomQuestion();
    }

    public void AnswerOption(int index = 0)
    {
        if (index == -1) {
            Debug.LogWarning("Invalid Answer button Clicked");
            return;
        }

        resultText.text = index == correctIndex ? "Correct!":"Wrong!";

        infoText.text = activeQuestionData.info; 


    }
    public void LoadRandomQuestion(int alternatives = 2)
    {
        QuestionData questionData = ItemDictionary.GetRandomQuestionData();
        if (questionData == null) {
            Debug.Log("Could not load any Question!");
            return;
        }

        activeQuestionData = questionData;

        infoText.text = "";

        UpdateCurrentQuestionVisuals();

    }

    private void CreateButtons(int alternatives)
    {
        for (int i = buttons.Length - 1; i >= 0; i--) {
            Destroy(buttons[i].gameObject);
        }

        buttons = new ButtonOption[alternatives];
        for (int i = 0; i < alternatives; i++) {
            ButtonOption option = Instantiate(buttonPrefab,buttonHolder.transform);
            option.UpdateIndex(i);
            option.SetGame(this);
            buttons[i] = option;            
        }
    }

    private void UpdateCurrentQuestionVisuals(int alternatives=2)
    {
        // Make correct amount of answer buttons
        CreateButtons(alternatives);

        resultText.text = "Make your Choice!";

        // Set Correct answer at random position and store the correct answer
        correctIndex = Random.Range(0, alternatives);

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
        if(activeQuestionData.sprites.Length > 0)
            QuestionImage.sprite = activeQuestionData.sprites[0];
    }

    private void PrintAnswers(List<string> listItems, string prefix="") => Debug.Log(prefix+" [" + string.Join(',', listItems) + "]");
}
