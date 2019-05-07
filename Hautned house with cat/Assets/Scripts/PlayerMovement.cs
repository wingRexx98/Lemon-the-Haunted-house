using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;// too slow then won't turn

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;//Quaternion is used to store rotation, current ly has no value indicated by Quaternion.identity

    AudioSource footStep;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        footStep = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();// make sure that the lenght of the vector 3 is either 1 or 0, normalize to make sure thelenght or magnetude of the vector is never > 1

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);// return true if the horizontal input is different from 0
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);// return true if the vertical input is different from 0
        bool isWalking = hasHorizontalInput || hasVerticalInput;// return true if the vertical or horizontal input is different from 0
        m_Animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            footStep.Play();
        }
        else
            footStep.Stop();

        // calculate the forward vector 
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);// create a rotation in the way of the new vector 3
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude); // deltaPosition is the change to the root in the last frame
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
