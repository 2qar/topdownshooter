using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour 
{
    public static GameObject enemy;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    public static void SpawnWave()
    {
        enemy = (GameObject)Resources.Load("Prefabs/Enemy");
        for (int i = 1; i <= 50; i++)
            Instantiate(enemy, new Vector3(100, 100, 0), Quaternion.Euler(0, 0, 0));
    }

    /// <summary>
    /// Spawns a given amount of enemies inside the test room.
    /// </summary>
    /// <param name="enemies">
    /// Amount of enemies to spawn.
    /// </param>
    public static void SpawnEnemiesInRoom(int enemies)
    {
        for (int spawns = 0; spawns < enemies; spawns++)
        {
            // generate a random spawn position around the player
            Vector2 spawnPos = new Vector2(1000, 1000);
            do
            {
                spawnPos.x = Random.Range(-120, 121);
                spawnPos.y = Random.Range(-89, 90);
            } while ((spawnPos.x > -30 && spawnPos.x < 30) && (spawnPos.y > -30 && spawnPos.y < 30));

            // spawn an enemy at the randomly picked spawn position
            Instantiate((GameObject)Resources.Load("Prefabs/Enemy"), spawnPos, Quaternion.identity);
        }
    }
}
