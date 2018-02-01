using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float speed;

    private void Start()
    {
        Rigidbody rigidBodyComponent = GetComponent<Rigidbody>();
        rigidBodyComponent.velocity = transform.forward * speed;
    }

}
