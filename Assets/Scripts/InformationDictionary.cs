using System.Collections.Generic;
using UnityEngine;
public class InformationDictionary : MonoBehaviour
{
    [Header("Information Data")]
    [SerializeField] List<InformationData> informationDatas;    
    
    [Header("Singleton")]
    public static InformationDictionary Instance { get; private set; }
    public int ActiveIndex { get; set; } = 0;
    public int InformationToatal => informationDatas.Count;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogWarning("Duplicate ItemDictionary!");
            Destroy(gameObject);
            return;
        }
        Instance = this;        
    }
    public static InformationData GetCurrent() => Instance.informationDatas[Instance.ActiveIndex];
    public static InformationData GetNext()
    {
        Instance.ActiveIndex = (Instance.ActiveIndex + 1)% Instance.informationDatas.Count;
        return Instance.informationDatas[Instance.ActiveIndex];
    }
}
