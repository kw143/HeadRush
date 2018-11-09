using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chase : EnemyVehicleController
{

    public Transform vehicle;
    //new private GameObject player;
    private int MoveSpeed = 21;
    private int MaxDist = 10;
    private int MinDist = 5;
    private int detectDist = 150;
    private float startingHealth = 10f;


    new void Death()
    {
        this.GetComponent<EnemyDeath>().triggerDeath = true;
        speed = 0;

    }

    void Start()
    {
        player = GameObject.Find("Vehicle");
        vehicle = player.GetComponent<Transform>();
        base.Health = startingHealth;
        base.Rb = gameObject.GetComponent<Rigidbody>();
    }



    new void Update()
    {
        
        if (base.Health <= 0)
        {
            Death();
        }
        MoveZDir(speed, 1);
        //This ensures they can only collide with landscape layer
        int layerMask = 1 << 9;
        //The Raycast hit for the front "Thruster"
        RaycastHit fhit;
        //Casting rays to get ground information
        Physics.Raycast(transform.position, Vector3.down, out fhit, Mathf.Infinity, layerMask);

        //Adding our own gravity
        Rb.AddForceAtPosition(Vector3.up * -5 * Mathf.Min(fhit.distance, 270), transform.position);
        //Adding thrust upward
        Rb.AddForceAtPosition(transform.up * (5000 / fhit.distance), transform.position);
        //Adding a dampening force
        Rb.AddForceAtPosition(Vector3.up * -2.5f * Rb.velocity.y, transform.transform.position);
        transform.LookAt(vehicle);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 bwd = transform.TransformDirection(Vector3.back);
        Vector3 lwd = transform.TransformDirection(Vector3.left);
        Vector3 rwd = transform.TransformDirection(Vector3.right);
        Rigidbody rrb = base.Rb;
        int layerMask2 = 1 << 8;
        layerMask2 = ~layerMask2;
        if (Vector3.Distance(transform.position, vehicle.position) >= MinDist)
        {

            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            //this.MoveZDir(MoveSpeed, 1);




            if (Vector3.Distance(transform.position, vehicle.position) <= MaxDist)
            {
                //Shoot at here or something
            }

            if (Physics.Raycast(transform.position, fwd, detectDist, layerMask2))
            {
                rrb.AddForce(transform.right * speed * -2500 * Time.deltaTime);
            }
            else if (Physics.Raycast(transform.position, bwd, detectDist, layerMask2))
            {
                rrb.AddForce(transform.forward * speed * 2500 * Time.deltaTime);
            }
            else if (Physics.Raycast(transform.position, lwd, detectDist, layerMask2))
            {
                rrb.AddForce(transform.forward * speed * 2500 * Time.deltaTime);
            }
            else if (Physics.Raycast(transform.position, rwd, detectDist, layerMask2))
            {
                rrb.AddForce(transform.right * speed * -2500 * Time.deltaTime);
            }
            else
            {
                this.MoveZDir(MoveSpeed, 1);
            }
        }

    }
}