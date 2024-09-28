using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastManager : MonoBehaviour
{
    public Player player;
    public GameObject monkey;
    public GameObject skunk;
    // public GameObject elephant;
    public GameObject bear;
    public Animator[] animators;

    public bool skunkUnlocked = true;
    // public bool elephantUnlocked = true;
    public bool bearUnlocked = true;

    public float cooldownSkunk = 2f;
    // public float cooldownElephant = 3f;
    public float cooldownBear = 4f;

    public float timeoutSkunk = 5f;
    // public float timeoutElephant = 10f;
    public float timeoutBear = 7f;

    float[] cooldownTimers;
    float timeoutTimer;


    void Awake()
    {
        player.currentBeast = monkey;
        player.currentBeast.SetActive(true);

        cooldownTimers = new float[] {0f, 0f, 0f, 0f};
    }

    void Update()
    {
        Switch();
        for (int i = 0; i < cooldownTimers.Length; i++)
            cooldownTimers[i] -= Time.deltaTime;
        timeoutTimer -= Time.deltaTime;

        if (timeoutTimer <= 0 && player.currentBeast != monkey)
        {
            SwitchToAnimal(monkey);
            timeoutTimer = 0f;
        }
        //Debug.Log(player.currentBeast);
        //Debug.Log(string.Join(", ", cooldownTimers));
    }

    void Switch()
    {
        if (player.currentBeast == monkey) cooldownTimers[0] = 0f;
        else if (player.currentBeast == skunk) cooldownTimers[1] = cooldownSkunk;
        // else if (player.currentBeast == elephant) cooldownTimers[2] = cooldownElephant;
        else if (player.currentBeast == bear) cooldownTimers[2] = cooldownBear;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (player.currentBeast == monkey) return;
            SwitchToAnimal(monkey);
            timeoutTimer = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && skunkUnlocked && cooldownTimers[1] <= 0)
        {
            if (player.currentBeast == skunk) return;
            SwitchToAnimal(skunk);
            timeoutTimer = timeoutSkunk;
        }

        //else if (Input.GetKeyDown(KeyCode.Alpha3) && elephantUnlocked && cooldownTimers[2] <= 0)
        //{
        //    if (player.currentBeast == elephant) return;
        //    SwitchToAnimal(elephant);
        //    timeoutTimer = timeoutElephant;
        //}

        else if (Input.GetKeyDown(KeyCode.Alpha4) && bearUnlocked && cooldownTimers[2] <= 0)
        {
            if (player.currentBeast == bear) return;
            SwitchToAnimal(bear);
            timeoutTimer = timeoutBear;
        }
    }

    void SwitchToAnimal(GameObject beast)
    {
        if (player.currentBeast == beast) return;
        if (player.currentBeast != null)
            player.currentBeast.SetActive(false);

        player.currentBeast = beast;
        player.currentBeast.SetActive(true);
    }
}
