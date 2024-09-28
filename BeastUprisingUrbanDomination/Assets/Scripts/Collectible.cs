using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool isCollected = false;
    public bool isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && !isCollected && collision.gameObject.CompareTag("Player"))
            isCollected = true;
    }
}
