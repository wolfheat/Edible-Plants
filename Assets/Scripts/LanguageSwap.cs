using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System;
using System.Collections;

public class LanguageSwap : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI languageName;
    [SerializeField] private Image flagImage;
    [SerializeField] Sprite[] flags;

    private Coroutine swaptimer;
    private const float SwapTime = 0.4f;

    public void SwapLanguage()
    {
        if (swaptimer != null)
            return;

        swaptimer = StartCoroutine(RunSwapTimer());

        Settings.Instance.SwapLanguage();
        ChangeLocalization();
    }

    private IEnumerator RunSwapTimer()
    {
        float startTime = Time.time;
        while (Time.time-startTime < SwapTime)
            yield return null;
        swaptimer = null;
    }

    private void ChangeLocalization() => LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[Settings.Instance.LanguageIndex];//languageName.text = Settings.Instance.Language;//flagImage.sprite = flags[Settings.Instance.LanguageIndex];
}
