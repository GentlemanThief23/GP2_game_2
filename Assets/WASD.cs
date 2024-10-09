using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling.Experimental;

public class WASD : MonoBehaviour
{
    public bool debugs = true;
    Rigidbody rb;
    float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddForce(Direction(debugs) * speed);
    }

    Vector3 Direction(bool debugs)
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        Debug.Log("raw input: " + h + " . " + v);

        if (debugs)
        {
            Debug.Log("vector: " + dir);
            Debug.DrawRay(transform.position, dir * 2f, Color.white);
            Debug.DrawRay(transform.position, rb.velocity, Color.yellow);
        }
        return dir;
    }
}

