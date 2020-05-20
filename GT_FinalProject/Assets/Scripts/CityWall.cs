using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityWall : MonoBehaviour
{
    public GameObject WallPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 0; y < 10; y += 2)
        {
            for (int i = -25; i < 26; i += 50)
            {
                for (int j = -24; j < 26; j += 2)
                {
                    Instantiate(WallPrefab, new Vector3(i, y, j), Quaternion.identity);
                }
                for (int j = -24; j < 26; j += 2)
                {
                    Instantiate(WallPrefab, new Vector3(j, y, i), Quaternion.identity);
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
