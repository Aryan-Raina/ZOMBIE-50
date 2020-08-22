using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float duration = 2f;


    void Update()
    {
        Destroy(gameObject, duration);
    }
}
