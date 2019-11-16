using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    public float floatAmount = 0.05f;
    public float bobSpeed = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += (Mathf.Sin(Time.time * bobSpeed) * Time.deltaTime) * floatAmount;
        transform.position = newPosition;

        transform.Translate(-Vector3.forward * Time.deltaTime);
    }
}
