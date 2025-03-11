using UnityEngine;

[CreateAssetMenu(menuName = "InformationData")]
public class InformationData : ScriptableObject
{
    [Header("Main Info")]
    public string InformationHeader = "";

    [TextArea(15, 20)]
    public string info;
    public Sprite sprite;

}
