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

    void Start()
    {
        player.currentAnimal = monkey;
        player.currentAnimal.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchToAnimal(monkey);
        
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchToAnimal(skunk);
        
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchToAnimal(elephant);
        
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchToAnimal(lion);
    }

    void SwitchToAnimal(GameObject animal)
    {
        if (player.currentAnimal != null)
            player.currentAnimal.SetActive(false);

        player.currentAnimal = animal;
        player.currentAnimal.SetActive(true);
    }
}
