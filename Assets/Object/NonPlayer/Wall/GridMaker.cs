using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour
{

    public GameObject road;
    public int panjang, lebar;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        for (int i = 0; i < lebar; i++)
        {
            for (int j = 0; j < panjang; j++)
            {
                float xPos = transform.position.x;
                float yPos = transform.position.y;
                Vector3 pos = new Vector3(xPos + 2 + (i * road.transform.localScale.x), yPos + 2 + (j * road.transform.localScale.y), 0);
                Instantiate(road, pos, Quaternion.identity);
            }
        }
    }

}
