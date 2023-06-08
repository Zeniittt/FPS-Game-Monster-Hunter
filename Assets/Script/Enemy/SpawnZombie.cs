using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombie : MonoBehaviour, ISpawnZombie
{
    public GameObject zombiePrefab;

    public ISpawnZombie Clone()
    {
        GameObject zombieObject = Instantiate(zombiePrefab, transform.position, transform.rotation);
        SpawnZombie zombie = zombieObject.GetComponent<SpawnZombie>();
        return zombie;
    }

    public void SpawnEnemy()
    {
        // Khi titan chết, tạo ra các đối tượng zombie
        ISpawnZombie[] zombies = new ISpawnZombie[4];
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i] = Clone();
        }
    }
}
