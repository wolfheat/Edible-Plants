using System.Collections.Generic;
using UnityEngine;

public class ItemDictionaryLoader : MonoBehaviour
{
    [SerializeField] private ItemDictionary itemDictionary;

    [ContextMenu("Reload Plants")]
    private void LoadAllPlantsAssetDatas()
    {
        Debug.Log("Reloading Plants");
        List<QuestionData> questions = PlantDataReader.ReturnAllData();
        itemDictionary.UpdatePlantsAssetDatas(questions);
    }
}
