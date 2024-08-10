using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BlockEffect : MonoBehaviour
{
    public float destroyDelay = 0.5f; // Duration of the explosion animation

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
