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

    [Header("AI Settings")]
    public float moveSpeed;
    public float avoidanceDistance = 1f;
    public LayerMask obstacleLayer;
    private Transform obstacleDetector;
    private void Awake()
    {
        obstacleDetector = GetComponent<Transform>();
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
    }
    IEnumerator chaseTarget()
    {
        GameObject target = currentTarget.gameObject;
        RaycastHit2D hitLeft = Physics2D.Raycast(obstacleDetector.position, Vector2.left, avoidanceDistance, obstacleLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(obstacleDetector.position, Vector2.right, avoidanceDistance, obstacleLayer);
        float distance = Vector2.Distance(transform.position, target.transform.position);
        while (distance > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            if (hitLeft.collider != null)
            {
                // Avoid obstacle by moving right
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
            else if (hitRight.collider != null)
            {
                // Avoid obstacle by moving left
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            }
        }
        
        yield return null;
    }
}
