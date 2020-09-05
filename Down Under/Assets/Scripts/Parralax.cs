using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    public Transform cam;
    public float depth = 10;
    private float x0;

    void Start()
    {
        x0 = transform.position.x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float deltaCam = x0 - cam.transform.position.x;
        transform.position = new Vector3(x0 + deltaCam * depth, transform.position.y, transform.position.z);
    }
}
