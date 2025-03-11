using TMPro;
using UnityEngine;

public class InformationPages : MonoBehaviour
{
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private DotController dotController;

    private void OnEnable() => LoadInfo(InformationDictionary.GetCurrent());

    public void LoadNextInfo() => LoadInfo(InformationDictionary.GetNext());

    private void LoadInfo(InformationData informationData)
    {
        if(informationData == null) {
            Debug.Log("Information data null");
            return;
        }
        headerText.text = informationData.InformationHeader;
        infoText.text = informationData.info;

        dotController.UpdateActiveDot();
    }
}
