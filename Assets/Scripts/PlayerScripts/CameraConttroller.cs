using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConttroller : MonoBehaviour
{
    public float camSpeed;

    public Transform player;

    PlayerMovementController playerMovement;

    Camera cam;
    int FOV = 60;

    void Start()
    {
        cam = this.GetComponent<Camera>();
        cam.fieldOfView = FOV;
    }

    void Update()
    {
        float curFOV = Mathf.Lerp(cam.fieldOfView, FOV + Mathf.Abs(Vector3.Distance(player.transform.position, Vector3.zero)), camSpeed * Time.deltaTime);

        cam.fieldOfView = curFOV;
    }
}
