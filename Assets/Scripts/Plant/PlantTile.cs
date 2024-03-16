using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTile : MonoBehaviour
{
    [Header("Plant Settings")]
    public PlantObject plantObject;
    public bool isReady = false;
    public bool isPlant = false;

    [Header("Sprite Settings")]
    public Sprite defaultSprite;
    private SpriteRenderer plantRenderer;
    private void Awake()
    {
        plantRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        
    }
    public void plantHarvest()
    {
        isPlant = false;
        isReady = false;
    }
    public void StartPlanting(PlantObject plantData)
    {
        plantObject = plantData;
        isPlant = true;
        StartCoroutine(plantGrowing(plantObject.plantDuration, plantObject.plantSprites));
    }
    IEnumerator plantGrowing(float duration, Sprite[] plants)
    {
        float currenTime = duration;
        while(currenTime > 0)
        {
            currenTime -= Time.deltaTime;
            float percentage = currenTime / duration;
            int index = (int)((1 - percentage) * plants.Length);
            index = Mathf.Min(index, plants.Length - 1);
            plantRenderer.sprite = plants[index];
        }
        isReady = true;
        yield return null;
    }
}
