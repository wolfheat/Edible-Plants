using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOption : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private GameObject selectedVisual;
    private BaseGame game;

    public void UpdateText(string s) => buttonText.text = s;
    public void SetGame(BaseGame gameToSet) => game = gameToSet;

    public void ClickButton()
    {
        if(game.HaveInputtedAnswer)
            return;

        bool correct = game.AnswerOption(buttonText.text);

        selectedVisual.SetActive(true);
        if(selectedVisual.TryGetComponent<Image>(out Image image))
            image.color = correct ? Settings.Instance.CorrectColor: Settings.Instance.WrongColor;

    }

    internal string GetText() => buttonText.text;
}
