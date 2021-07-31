using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementController : MonoBehaviour
{
    public static float playerSpeed = 3;
    public static bool canMove;
    
    Rigidbody rb;
    NavMeshAgent agent;

    void Awake()
    {
        canMove = false;
        rb = this.GetComponent<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canMove)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                MoveCar(hit.point);
            }
        }

        agent.speed = playerSpeed;
    }

    public void MoveCar(Vector3 pos)
    {
        agent.destination = pos;
    }
}
