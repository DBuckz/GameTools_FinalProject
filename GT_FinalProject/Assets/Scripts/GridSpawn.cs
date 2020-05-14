using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawn : MonoBehaviour
{
    public int gX = 4;
    public int gZ = 4;
    public GameObject SpawnPrefab;
    public Vector3 GridStart = Vector3.zero;
    public float gridDist = 2f;
    public bool GenerateOn;


    void OnEnable()
    {
        if (GenerateOn)
        {
            Generate();
        }
    }

    public void Generate()
    {
        SpawnGrid();
    }


    void SpawnGrid()
    {
        for (int x = 0; x < gX; x++)
        {
            for (int z = 0; z < gZ; z++)
            {
                GameObject clone = Instantiate(SpawnPrefab,
                    transform.position + GridStart + new Vector3(gridDist * x, 0, gridDist * z), transform.rotation);
                clone.transform.SetParent(this.transform);
            }
        }
    }
}
