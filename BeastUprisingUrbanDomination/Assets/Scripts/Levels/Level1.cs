using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level1 : MonoBehaviour
{
    public List<GameObject> collectibles;
    public List<GameObject> enemies;
 
    void Update()
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            if (collectibles[i].GetComponent<Collectible>().isCollected)
            {
                Destroy(collectibles[i]);
                collectibles.RemoveAt(i);
            }
        }
           
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].GetComponent<Enemy>().isDead)
            {
                enemies.RemoveAt(i);
            }
        }

        if (collectibles.Count == 0 && enemies.Count == 0)
        {
            Debug.Log("Level complete!");
            SceneManager.LoadScene("Scenes/Final Levels/Level2");
        }
    }
}
