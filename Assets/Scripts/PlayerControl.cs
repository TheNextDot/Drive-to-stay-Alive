using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float accelerationForce;
    public float brakeFactor;
    public float dragConstant;
    public float steeringForce;

    private Rigidbody2D rigidbody;
    private List<Vector2> forces = new List<Vector2>();
    private float dragFactor = 0;

    void FixedUpdate()
    {
        UpdateAcceleration();
        UpdateDirection();
        CommitForces();
    }

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void UpdateAcceleration()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            AddForwardForce();
        } else if (Input.GetAxis("Vertical") < 0)
        {
            Brake();
        }
        UpdateDragForce();
    }

    private void AddForwardForce()
    {
        forces.Add(new Vector2(0, accelerationForce));
    }

    private void Brake()
    {
        dragFactor = dragConstant + brakeFactor;
    }

    private void UpdateDragForce()
    {
        if (dragFactor == 0) { dragFactor = dragConstant; }
        forces.Add(new Vector2(0, - dragFactor * (float)Math.Max(Math.Pow(rigidbody.velocity.magnitude, 2), rigidbody.velocity.magnitude)));
    }

    private void UpdateDirection()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            //Steer(Input.GetAxis("Horizontal"));
        }
    }

    private void CommitForces()
    {
        rigidbody.AddForce(AggregateForces());
        forces = new List<Vector2>();
    }

    private Vector2 AggregateForces()
    {
        return forces.Aggregate((f1, f2) => f1 + f2);
    }
}
