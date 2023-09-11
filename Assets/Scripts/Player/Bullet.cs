using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject owner;
    public int Darmage;
    public bool locktraget;

    private void FixedUpdate()
    {
        if(locktraget)
            transform.Translate(Vector3.up * 10f * Time.fixedDeltaTime) ;
    }
}
