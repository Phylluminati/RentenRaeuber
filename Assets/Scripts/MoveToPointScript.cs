using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Yarn.Unity;

public class MoveToPointScript : MonoBehaviour
{
    public float followSharpness = 0.1f;
    
    // [SerializeField]
    Animator myAnimator;
    Rigidbody myRigidbody;

    Vector3 endPosition;
    Vector3 targetPosition;
    bool isMoving;

    public GameObject TestWaypoint;
    private GameObject nWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        MoveToPoint(TestWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            targetPosition = nWaypoint.transform.position;
            targetPosition.y = transform.position.y;
            if (targetPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            transform.position += (targetPosition - transform.position) * followSharpness;


            //myAnimator.SetBool("isWalking", endPosition != targetPosition);

            endPosition = targetPosition;
            if (targetPosition.x == transform.position.x && targetPosition.z == transform.position.z)
            {
                isMoving = false;
            }

        }
        
        myAnimator.SetBool("isWalking", endPosition != targetPosition);

    }

    [YarnCommand("MoveTo")]
    public void MoveToPoint(GameObject waypoint)
    {
        nWaypoint = waypoint;
        targetPosition = waypoint.transform.position;
        myAnimator = this.GetComponentInChildren<Animator>();
        myRigidbody = this.GetComponentInChildren<Rigidbody>();
        isMoving = true;

        /*while (targetPosition.x != transform.position.x && targetPosition.z != transform.position.z)
        {
            targetPosition = waypoint.transform.position + _followOffset;
            targetPosition.y = transform.position.y;
            if (targetPosition.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            transform.position += (targetPosition - transform.position) * followSharpness;


            //myAnimator.SetBool("isWalking", endPosition != targetPosition);

            endPosition = targetPosition;
            print("LOOP");
        }
        print("MethodDone");

        myAnimator.SetBool("isWalking", endPosition != targetPosition);*/



    }

    [YarnCommand("Teleport")]
    public void Teleport(GameObject waypoint)
    {
        transform.position = new Vector3(waypoint.transform.position.x, this.transform.position.y, waypoint.transform.position.z);
    }
}
