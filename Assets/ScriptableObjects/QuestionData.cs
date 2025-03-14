using System;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestionData")]
public class QuestionData : ScriptableObject
{
    [Header("Main Info")]
    public string ItemName;
    public string LatinName;
    [TextArea(11,10)]
    public string info;
    public Sprite[] sprites;

    [Header("Availability")]
    public int commonness;
    public bool feral;
    public bool protectedPlant;

    [Header("Edible Parts")]
    public int root;
    public int stem;
    public int leaf;
    public int flower;
    public int seed;
    public int fruit;
    public int avoid;
    public int sap;
    public int treeBush;
    public int fungi;

    [Header("Other Usages")]
    public int army;
    public string medicinal;
    public int allergenic;

    [Header("Categories")]
    public int CategoriesBinary;

    public int[] PlantParts => new int[]{root,stem,leaf,flower,seed,fruit,avoid,fungi};

    public void CreateCategories()
    {
        CategoriesBinary = 0;
        if (treeBush != 0)
            CategoriesBinary += 1;
        if (flower != 0)
            CategoriesBinary += 1 << 1;
        if (root != 0)
            CategoriesBinary += 1 << 2;
        if (seed != 0)
            CategoriesBinary += 1 << 3;
        if (fruit != 0)
            CategoriesBinary += 1 << 4;
        if (root == 1 || stem == 1 || leaf == 1 || flower == 1 || fruit == 1) // raw
            CategoriesBinary += 1 << 5;
        if (avoid != 0) // Avoid
            CategoriesBinary += 1 << 6;
        if (root > 1 || stem > 1 || leaf > 1 || flower > 1 || fruit > 1) // raw
            CategoriesBinary += 1 << 7;
        if(fungi != 0)
            CategoriesBinary += 1 << 8;
    }
}
