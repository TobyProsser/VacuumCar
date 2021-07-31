using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject poll;
    public GameObject camera;

    public bool finished;
    bool scored;
    int curScore = -1;

    PlayerMovementController playerMovement;

    public ParticleSystem[] sparkParts = new ParticleSystem[2];
    public bool jumped;

    public List<GameObject> playerParts = new List<GameObject>();

    bool died;

    public CanvasController canvasController;

    public ColorWave curColorWave;

    void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovementController>();
        finished = false;
        scored = false;
        curScore = -1;
    }

    void FixedUpdate()
    {
        if (this.transform.position.y < .2f && !died && !finished) OnDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    
    void OnDeath()
    {
        AudioManager.instance.Play("Break");
        died = true;
        print("dead");

        camera.transform.parent = null;
        camera.GetComponent<CameraConttroller>().camSpeed = 2;
        this.GetComponent<Rigidbody>().useGravity = false;

        for (int i = 0; i < playerParts.Count; i++)
        {
            GameObject curPart = playerParts[i];
            curPart.AddComponent<Rigidbody>();
            curPart.AddComponent<BoxCollider>();

            curPart.GetComponent<Rigidbody>().AddExplosionForce(10, curPart.transform.position, 6, 4);

            Destroy(curPart, 3);
        }

        CanvasController.timesPlayed++;
        canvasController.OpenBasePanel();
    }
}
