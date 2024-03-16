using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSide : MonoBehaviour
{
    [Header("NPC Settings")]
    private GameObject[] plantObject;
    public PlantTile[] plantTiles;
    private void Awake()
    {
        plantObject = GameObject.FindGameObjectsWithTag("Tiles");
        plantTiles = new PlantTile[plantObject.Length];
        for (int i = 0; i < plantTiles.Length; i++)
        {
            PlantTile tile = plantObject[i].GetComponent<PlantTile>();
            plantTiles[i] = tile;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
