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

    float[] cooldowns;
    float[] timeouts;
    float cooldownTimer;
    float timeoutTimer;


    void Start()
    {
        player.currentAnimal = monkey;
        player.currentAnimal.SetActive(true);

        cooldowns = new float[] {0f, cooldownSkunk, cooldownElephant, cooldownLion };
        timeouts = new float[] {0f, timeoutSkunk, timeoutElephant, timeoutLion};
    }

    void Update()
    {
        Switch();
        cooldownTimer -= Time.deltaTime;
        timeoutTimer -= Time.deltaTime;

        if (timeoutTimer <= 0 && player.currentAnimal != monkey)
        {
            SwitchToAnimal(monkey);
            timeoutTimer = 0f;
        }
        Debug.Log(player.currentAnimal);
        Debug.Log(cooldownTimer);
    }

    void Switch()
    {
        if (cooldownTimer > 0) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToAnimal(monkey);
            cooldownTimer = 0f;
            timeoutTimer = 0f;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2) && skunkUnlocked)
        {
            SwitchToAnimal(skunk);
            cooldownTimer = cooldownSkunk;
            timeoutTimer = timeoutSkunk;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3) && elephantUnlocked)
        {
            SwitchToAnimal(elephant);
            cooldownTimer = cooldownElephant;
            timeoutTimer = timeoutElephant;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && lionUnlocked)
        {
            SwitchToAnimal(lion);
            cooldownTimer = cooldownLion;
            timeoutTimer = timeoutLion;
        }
    }

    void SwitchToAnimal(GameObject animal)
    {
        if (player.currentAnimal == animal) return;
        if (cooldownTimer > 0) return;
        if (player.currentAnimal != null)
            player.currentAnimal.SetActive(false);

        player.currentAnimal = animal;
        player.currentAnimal.SetActive(true);
    }
}
