using System.Collections;
using UnityEngine;

public class FleeFromPlayer : MonoBehaviour {

    public Boundary boundary;
    private Vector3 velocityVector;
    private float currentSpeed;
    private Rigidbody rigidBody;
    public float minVelocity;
    public float maxVelocity;
    public float minWaitTime;
    public float maxWaitTime;
    public float minMovementTime;
    public float maxMovementTime;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentSpeed = rigidBody.velocity.y;
        StartCoroutine(Flee());
    }

    //The game object going to move randomly in an area determined by the boundary class
    IEnumerator Flee()
    {

        while (true)
        {
            //Calculate the new velocity to initiate movement
            velocityVector.x = Random.Range(minVelocity, maxVelocity) * -Mathf.Sign(this.transform.position.x);
            velocityVector.z = Random.Range(minVelocity, maxVelocity) * -Mathf.Sign(this.transform.position.z);
            //Wait until movement finished
            yield return new WaitForSeconds(Random.Range(minMovementTime, maxMovementTime));
            //Nullify velocity to stop movement
            velocityVector *= 0;
            //Wait before next movement
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    void FixedUpdate()
    {
        //calculate the movement move the initial velocity toward the new one depending on the delta time
        float newManeuverX = Mathf.MoveTowards(rigidBody.velocity.x, velocityVector.x, Time.deltaTime);
        float newManeuverZ = Mathf.MoveTowards(rigidBody.velocity.z, velocityVector.z, Time.deltaTime);
        //initiate the movement by appliying the new velocity vector
        rigidBody.velocity = new Vector3(newManeuverX, currentSpeed, newManeuverZ);
        //Adjust the gameobject position in order for it to be inside the area predetermined by the boundart
        rigidBody.position = new Vector3
        (
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax)
            , 0.0f
            , Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
        );

    }
}
