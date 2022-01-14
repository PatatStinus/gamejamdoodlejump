using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGeneration : MonoBehaviour
{
    [SerializeField] private List<GameObject> platforms = new List<GameObject>();
    [SerializeField] private Transform platformParent;

    [SerializeField] private Vector2 boundsWalls = new Vector2(-7.4f, 7.4f);
    [SerializeField] private float maxHeightbtnPfm, minHeightbtnPfm;

    private float lastPfmHeight = 0;

    private void Start()
    {
        while (lastPfmHeight < 1000)
        {
            GameObject spawnedPfm = Instantiate(platforms[Random.Range(0, platforms.Count)], platformParent);
            float x = Random.Range(boundsWalls.x, boundsWalls.y);
            float y = lastPfmHeight + Random.Range(minHeightbtnPfm, maxHeightbtnPfm);
            spawnedPfm.transform.position = new Vector2(x, y);

            lastPfmHeight = y;
        }
    }

    IEnumerator SpawnPlatforms()
    {

        yield return null;
    }
}
