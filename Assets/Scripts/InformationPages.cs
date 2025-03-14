using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationPages : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI headerText;

    [SerializeField] private DotController dotController;

    [SerializeField] private GameObject informationTextHolder;

    [SerializeField] private GameObject informationTextPrefab;
    [SerializeField] private InformationImage informationImagePrefab;

    private void OnEnable() => LoadInfo(InformationDictionary.GetCurrent());

    public void LoadNextInfo() => LoadInfo(InformationDictionary.GetNext());
    public void LoadPrevInfo() => LoadInfo(InformationDictionary.GetPrevious());

    private void LoadInfo(InformationData informationData)
    {
        if(informationData == null) {
            Debug.Log("Information data null");
            return;
        }

        headerText.text = informationData.InformationHeader;

        RemoveAll();

        ShowTextAndImages(informationData);
                
        StartCoroutine(dotController.UpdateActiveDot());
    }

    private void ShowTextAndImages(InformationData informationData)
    {
        Debug.Log("Updating whats shown in Information Pages");

        // Split the text into [] pieces

        char[] chars = informationData.info.ToCharArray();

        bool readNumber = false;
        
        List<char> textBuilder = new();

        foreach (char c in chars) {
            //Debug.Log("Reading: "+c);
            if (c == ']') {
                readNumber = false;
                continue;
            }
            if (readNumber) {
                Debug.Log("This is a number");
                int spriteIndex = c-'0';
                // Skip if there is no sprite in the dats to load
                if(informationData.sprites.Length < spriteIndex+1 || informationData.sprites[spriteIndex] == null) {
                    continue;
                }

                // Create a new image for the text
                InformationImage picture = Instantiate(informationImagePrefab, informationTextHolder.transform);
                picture.SetImageSprite(informationData.sprites[spriteIndex]);
                Debug.Log("Creating Picture: "+ informationData.sprites[spriteIndex].name);
                continue;
            }
            if(c == '[') {
                readNumber = true;
                
                if (textBuilder.Count == 0)
                    continue;

                // Create a new text object if there is loaded text
                GameObject textPart = Instantiate(informationTextPrefab, informationTextHolder.transform);
                textPart.GetComponent<TextMeshProUGUI>().text = new String(textBuilder.ToArray());
                Debug.Log("Creating Text: "+textBuilder.ToString());

                textBuilder.Clear();
                continue;
            }
            textBuilder.Add(c);
        }
        if (textBuilder.Count == 0)
            return;

        // Create a new text object if there is loaded text
        GameObject textPartEnd = Instantiate(informationTextPrefab, informationTextHolder.transform);
        textPartEnd.GetComponent<TextMeshProUGUI>().text = new String(textBuilder.ToArray());
        Debug.Log("Creating Text: " + textBuilder.ToString());


    }

    private void RemoveAll()
    {
        Debug.Log("Removing all text and images from the Information Pages text holder");
        foreach (Transform t in informationTextHolder.transform) {
            if (t == this.transform) continue;
            Destroy(t.gameObject);
        }
    }
}
