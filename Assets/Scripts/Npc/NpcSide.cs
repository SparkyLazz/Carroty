using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSide : MonoBehaviour
{
    [Header("NPC Settings")]
    public PlantTile currentTarget;
    private GameObject[] plantObject;
    private PlantTile[] plantTiles;

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
    private void Start()
    {
        getRandomTarget();
    }
    private void getRandomTarget()
    {
        List<int> validIndices = new List<int>();
        System.Random random = new System.Random();
        for (int i = 0; i < plantTiles.Length; i++)
        {
            if (!plantTiles[i].isPlant || plantTiles[i].isReady)
            {
                validIndices.Add(i);
                continue;
            }
        }
        int randomIndex = validIndices[random.Next(0, validIndices.Count)];
        currentTarget = plantTiles[randomIndex];
        StartCoroutine(chasingTarget());
    }
    IEnumerator chasingTarget()
    {
        Vector2 targetPosition = currentTarget.gameObject.transform.position;
        float speed = 5f; // Speed at which the NPC moves towards the target

        while (Vector2.Distance(gameObject.transform.position, targetPosition) > 0.1f) // Use a small threshold to avoid floating point precision issues
        {
            // Move the NPC towards the target
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, targetPosition, speed * Time.deltaTime);

            Debug.Log("Chasing");
            yield return null; // This allows the coroutine to pause here, letting other game processes to run
        }
        Debug.Log("Done");
    }

}
