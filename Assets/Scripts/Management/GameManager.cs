using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameRunning;

    public ParticleSystem dustParticle;
    public PlayerMovementController movementController;

    public CanvasController canvasController;

    LevelManager levelManager;

    void Awake()
    {
        levelManager = this.GetComponent<LevelManager>();
    }

    void Update()
    {
        if (dustParticle.particleCount <= 0 && gameRunning)
        {
            CompletedLevel();
        }
    }

    void CompletedLevel()
    {
        AudioManager.instance.Play("BeatLevel");

        dustParticle.Stop();

        gameRunning = false;

        PlayerMovementController.canMove = false;
        movementController.MoveCar(new Vector3(0, 0.91f, -8.42f));

        SavedData.savedData.level++;

        canvasController.OpenBasePanel();

        SavedData.savedData.coins += 2;
    }

    public void LoadNextLevel()
    {
        //Change color and amount of drops
        dustParticle.Play();

        PlayerMovementController.canMove = true;

        levelManager.NextLevel();

        //Make sure that game starts only after some dust has spawned
        //this prevents the end condition being met at the beginning of the level
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        while (true)
        {
            if (dustParticle.particleCount >= 10) break;

            yield return null;
        }


        gameRunning = true;
    }
}
