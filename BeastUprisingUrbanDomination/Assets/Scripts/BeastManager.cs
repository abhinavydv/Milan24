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

    float cooldownTimer;
    float[] cooldownTimers;
    float timeoutTimer;


    void Start()
    {
        player.currentAnimal = monkey;
        player.currentAnimal.SetActive(true);

        cooldownTimers = new float[] {0f, 0f, 0f, 0f};
    }

    void Update()
    {
        Switch();
        for (int i = 0; i < cooldownTimers.Length; i++)
            cooldownTimers[i] -= Time.deltaTime;
        timeoutTimer -= Time.deltaTime;

        if (timeoutTimer <= 0 && player.currentAnimal != monkey)
        {
            SwitchToAnimal(monkey);
            timeoutTimer = 0f;
        }
        Debug.Log(player.currentAnimal);
        Debug.Log(string.Join(", ", cooldownTimers));
    }

    void Switch()
    {
        if (player.currentAnimal == monkey) cooldownTimers[0] = 0f;
        else if (player.currentAnimal == skunk) cooldownTimers[1] = cooldownSkunk;
        else if (player.currentAnimal == elephant) cooldownTimers[2] = cooldownElephant;
        else if (player.currentAnimal == lion) cooldownTimers[3] = cooldownLion;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (player.currentAnimal == monkey) return;
            SwitchToAnimal(monkey);
            timeoutTimer = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && skunkUnlocked && cooldownTimers[1] <= 0)
        {
            if (player.currentAnimal == skunk) return;
            SwitchToAnimal(skunk);
            timeoutTimer = timeoutSkunk;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && elephantUnlocked && cooldownTimers[2] <= 0)
        {
            if (player.currentAnimal == elephant) return;
            SwitchToAnimal(elephant);
            timeoutTimer = timeoutElephant;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && lionUnlocked && cooldownTimers[3] <= 0)
        {
            if (player.currentAnimal == lion) return;
            SwitchToAnimal(lion);
            timeoutTimer = timeoutLion;
        }
    }

    void SwitchToAnimal(GameObject animal)
    {
        if (player.currentAnimal == animal) return;
        if (player.currentAnimal != null)
            player.currentAnimal.SetActive(false);

        player.currentAnimal = animal;
        player.currentAnimal.SetActive(true);
    }
}
