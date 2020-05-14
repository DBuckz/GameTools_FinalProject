using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PerlinNoise : MonoBehaviour
{
    public static PerlinNoise instance = null;

    public int perlinSX;
    public int perlinSY;
    public bool noiseRandom;
    public Vector2 pOffset;
    public float noiseF = 10f;
    public int gridX = 4;
    public int gridY = 4;

    public bool visualizeGrid = false;
    public GameObject visualizationCube;
    public float visualizationHeightScale = 5f;
    //public RawImage visualizationUI;


    private Texture2D perlinTexture;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Generate()
    {
        GenerateNoise();
        if (visualizeGrid)
        {
            VisualizeGrid();
        }
    }

    void GenerateNoise()
    {
        if (noiseRandom)
        {
            pOffset = new Vector2(Random.Range(0, 99999), Random.Range(0, 99999));
        }

        perlinTexture = new Texture2D(perlinSX, perlinSY);

        for (int x = 0; x < perlinSX; x++)
        {
            for (int y = 0; y < perlinSY; y++)
            {
                perlinTexture.SetPixel(x, y, SampleNoise(x, y));
            }
        }

        perlinTexture.Apply();
        //visualizationUI.texture = perlinTexture;
    }

    Color SampleNoise(int x, int y)
    {
        float xCoord = (float)x / perlinSX * noiseF + pOffset.x;
        float yCoord = (float)y / perlinSY * noiseF + pOffset.y;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        Color perlinColor = new Color(sample, sample, sample);

        return perlinColor;
    }

    public float SampleStepped(int x, int y)
    {
        int gridStepSizeX = perlinSX / gridX;
        int gridStepSizeY = perlinSY / gridY;

        float sampledFloat = perlinTexture.GetPixel
                   ((Mathf.FloorToInt(x * gridStepSizeX)), (Mathf.FloorToInt(y * gridStepSizeX))).grayscale;

        return sampledFloat;
    }

    public float PerlinSteppedPosition(Vector3 worldPosition)
    {
        int xToSample = Mathf.FloorToInt(worldPosition.x + gridX * .5f);
        int yToSample = Mathf.FloorToInt(worldPosition.z + gridY * .5f);

        xToSample = xToSample % gridX;
        yToSample = yToSample % gridY;

        float sampledValue = SampleStepped(xToSample, yToSample);

        return sampledValue;
    }

    void VisualizeGrid()
    {
        GameObject visualizationParent = new GameObject("VisualizationParent");
        visualizationParent.transform.SetParent(this.transform);

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                GameObject clone = Instantiate(visualizationCube,
                    new Vector3(x, SampleStepped(x, y) * visualizationHeightScale, y)
                    + transform.position, transform.rotation);

                clone.transform.SetParent(visualizationParent.transform);
                CityManager.instance.AddObject(clone);
            }
        }

        visualizationParent.transform.position =
            new Vector3(-gridX * .5f, -visualizationHeightScale * .5f, -gridY * .5f);
    }

    /*public void SetnoiseFFromSlider(Slider slider)
    {
        noiseF = slider.value;
    }*/
}
