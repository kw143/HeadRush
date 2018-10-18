using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chase : EnemyVehicleController
{

    public Transform vehicle;
    private GameObject player;
    int MoveSpeed = 21;
    int MaxDist = 10;
    int MinDist = 5;
    int detectDist = 25;
    private float startingHealth = 60f;




    void Start()
    {
        player = GameObject.Find("Vehicle");
        vehicle = player.GetComponent<Transform>();
        base.Health = startingHealth;
        base.Rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        base.Update();
        transform.LookAt(vehicle);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 bwd = transform.TransformDirection(Vector3.back);
        Vector3 lwd = transform.TransformDirection(Vector3.left);
        Vector3 rwd = transform.TransformDirection(Vector3.right);
        Rigidbody rrb = gameObject.GetComponent<Rigidbody>();

        if (Vector3.Distance(transform.position, vehicle.position) >= MinDist)
        {

            //transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            this.MoveZDir(MoveSpeed, 1);




            if (Vector3.Distance(transform.position, vehicle.position) <= MaxDist)
            {
                //Shoot at here or something
            }

            if (Physics.Raycast(transform.position, fwd, detectDist))
                rrb.AddForce(transform.forward * speed * -2500 * Time.deltaTime);

            if (Physics.Raycast(transform.position, bwd, detectDist))
                rrb.AddForce(transform.forward * speed * 2500 * Time.deltaTime);

            if (Physics.Raycast(transform.position, lwd, detectDist))
                rrb.AddForce(transform.right * speed * 2500 * Time.deltaTime);

            if (Physics.Raycast(transform.position, rwd, detectDist))
                rrb.AddForce(transform.right * speed * -2500 * Time.deltaTime);
        }

    }
}