using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chip : MonoBehaviour
{
    private Vector3 _force;

    private void Start()
    {
        _force = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        GetComponent<Rigidbody>().AddForce(_force, ForceMode.Impulse);
        
        Invoke(nameof(Destroy), Random.Range(2f,3f));
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
