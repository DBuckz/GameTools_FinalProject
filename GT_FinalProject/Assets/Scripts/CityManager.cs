using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public static CityManager instance;
    public List<GameObject> generatedObjects = new List<GameObject>();

    public PerlinNoise perlinGenerator;
    public GridSpawn gridSpawner;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Generate();
    }

    public void AddObject(GameObject objectToAdd)
    {
        generatedObjects.Add(objectToAdd);
    }

    void Generate()
    {
        perlinGenerator.Generate();
        gridSpawner.Generate();
    }

    void ClearAllObjects()
    {
        for (int i = generatedObjects.Count - 1; i >= 0; i--)
        {
            generatedObjects[i].SetActive(false);
            generatedObjects.RemoveAt(i);
        }
    }
}
