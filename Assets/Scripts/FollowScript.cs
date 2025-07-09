using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform leader;
    public float followSharpness = 0.1f;

    Vector3 _followOffset;
    [SerializeField]
    Animator myAnimator;
    Rigidbody myRigidbody;

    Vector3 endPosition;

    void Start()
    {

        _followOffset = transform.position - leader.position;
        myAnimator = this.GetComponentInChildren<Animator>();
        myRigidbody = this.GetComponentInChildren<Rigidbody>();
        print(myAnimator);
    }

    void LateUpdate()
    {

        Vector3 targetPosition = leader.position + _followOffset;


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


        myAnimator.SetBool("isWalking", endPosition != targetPosition);

        endPosition = targetPosition;

    }

    
}
