using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastManager : MonoBehaviour
{
    public Player player;
    public GameObject monkey;
    public GameObject skunk;
    public GameObject elephant;
    public GameObject lion;

    public bool skunkUnlocked = true;
    public bool elephantUnlocked = true;
    public bool lionUnlocked = true;

    public float cooldownSkunk = 2f;
    public float cooldownElephant = 3f;
    public float cooldownLion = 4f;

    public float timeoutSkunk = 5f;
    public float timeoutElephant = 10f;
    public float timeoutLion = 7f;

    float[] cooldownTimers;
    float timeoutTimer;


    void Start()
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
        Debug.Log(player.currentBeast);
        Debug.Log(string.Join(", ", cooldownTimers));
    }

    void Switch()
    {
        if (player.currentBeast == monkey) cooldownTimers[0] = 0f;
        else if (player.currentBeast == skunk) cooldownTimers[1] = cooldownSkunk;
        else if (player.currentBeast == elephant) cooldownTimers[2] = cooldownElephant;
        else if (player.currentBeast == lion) cooldownTimers[3] = cooldownLion;

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

        else if (Input.GetKeyDown(KeyCode.Alpha3) && elephantUnlocked && cooldownTimers[2] <= 0)
        {
            if (player.currentBeast == elephant) return;
            SwitchToAnimal(elephant);
            timeoutTimer = timeoutElephant;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && lionUnlocked && cooldownTimers[3] <= 0)
        {
            if (player.currentBeast == lion) return;
            SwitchToAnimal(lion);
            timeoutTimer = timeoutLion;
        }
    }

    void SwitchToAnimal(GameObject animal)
    {
        if (player.currentBeast == animal) return;
        if (player.currentBeast != null)
            player.currentBeast.SetActive(false);

        player.currentBeast = animal;
        player.currentBeast.SetActive(true);
    }
}
