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


    protected QuestionData activeQuestionData = null;


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
