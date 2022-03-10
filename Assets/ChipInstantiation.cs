using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChipInstantiation : MonoBehaviour
{
    public static ChipInstantiation current;
    
    public List<GameObject> listOfObjects;

    public float xMin, xMax;
    public float yMin, yMax;
    public float zMin, zMax;
    
    private void Awake()
    {
        current = this;
    }

    public void SpawnChips(int numberToSpawn)
    {
        StartCoroutine(SpawnChips_Coroutine(numberToSpawn));
    }

    private IEnumerator SpawnChips_Coroutine(int numberToSpawn)
    {
        for (int ii = 0; ii < numberToSpawn; ii++)
        {
            GameObject chipToSpawn = listOfObjects[Random.Range(0, listOfObjects.Count)];
            Vector3 spawnPosition = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
            Quaternion spawnRotation = Quaternion.Euler(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360));

            Instantiate(chipToSpawn, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
