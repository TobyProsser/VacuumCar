using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DustParticleController : MonoBehaviour
{
    public float suckSpeed = 5;

    public GameObject cube;

    public Transform vacuum;
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();


    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            //p.startColor = new Color32(255, 0, 0, 255);
            p.remainingLifetime = 0;
            enter[i] = p;


            CreateDustAtParticle(p);
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(0, 255, 0, 255);
            
            exit[i] = p;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }

    void CreateDustAtParticle(ParticleSystem.Particle p)
    {
        /*
        for (int i = 0; i < 4; i++)
        {
            GameObject curObject = Instantiate(cube);
            curObject.transform.position = p.position + new Vector3(i/10, 0, 0);
            curObject.transform.localScale = p.startSize3D * .7f;
            curObject.transform.rotation = Quaternion.Euler(p.rotation3D.x, p.rotation3D.y, p.rotation3D.z);
            curObject.GetComponent<Renderer>().material.color = p.startColor;

            StartCoroutine(SuckInObject(curObject));
        }
        */

        GameObject curObject = Instantiate(cube);
        curObject.transform.position = p.position;
        curObject.transform.localScale = p.startSize3D *.25f;
        curObject.transform.rotation = Quaternion.Euler(p.rotation3D.x, p.rotation3D.y, p.rotation3D.z);
        curObject.GetComponent<Renderer>().material.color = p.startColor;

        StartCoroutine(SuckInObject(curObject));
    }

    IEnumerator SuckInObject(GameObject curObject)
    {
        while (curObject)
        {
            Vector3 objectPos = curObject.transform.position;
            Vector3 thisPos = vacuum.transform.position;

            float step = suckSpeed * Time.deltaTime;
            curObject.transform.position = Vector3.MoveTowards(objectPos, thisPos, step);

            if (Mathf.Abs(Vector3.Distance(objectPos, thisPos)) < 1f)
            {
                if(PlayerMovementController.playerSpeed < 5) PlayerMovementController.playerSpeed += .01f;

                print("true");
                Destroy(curObject);

                AudioManager.instance.Play("DustCollect");
            }

            yield return null;
        }
    }
}
