using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private GameObject popupObject;


    public static PopupText Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        ShowPopup("Start of Application.");
    }


    public void ShowPopup(string message)
    {
        if (Instance.routine != null) return;

        popupObject.SetActive(true);
        popupText.text = message;
        routine = StartCoroutine(ShowMessage());
    }

    private Coroutine routine;

    private IEnumerator ShowMessage()
    {
        yield return new WaitForSeconds(1.5f);
        Instance.popupObject.SetActive(false);
        routine = null;
    }
}
