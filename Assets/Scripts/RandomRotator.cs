using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{

    public float tumble;

    private void Start()
    {
        Rigidbody rigidBodyComponent = GetComponent<Rigidbody>();
        rigidBodyComponent.angularVelocity = Random.insideUnitSphere * tumble;
    }

}
