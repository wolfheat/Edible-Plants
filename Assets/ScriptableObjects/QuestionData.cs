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
    public int army;

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
    public int treeBush;


    [Header("Medicinal Usage")]
    public string medicinal;

    [Header("Categories")]
    public int CategoriesBinary;

    public int[] PlantParts => new int[]{root,stem,leaf,flower,seed,fruit,avoid};

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

    }
}
