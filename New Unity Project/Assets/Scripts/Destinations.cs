using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destinations : MonoBehaviour
{

    public GameObject prefab;
    public int amount = 10;
    public float range = 10f;

    GameObject[] prefabs;

    // Use this for initialization
    void Start()
    {
        prefabs = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            Vector2 randPos = Random.insideUnitCircle * range;
            Vector3 randPos3 = new Vector3(randPos.x, 0.05f, randPos.y);
            GameObject destination = (GameObject)Instantiate(prefab, randPos3, Quaternion.Euler(270, 0, 0));
            destination.name = "Dest " + destination.GetHashCode();
            prefabs[i] = destination;
        }
    }

    public Vector3 GetRandomDestination()
    {
        return prefabs[Mathf.FloorToInt(Random.value * amount)].transform.position;
    }

}
