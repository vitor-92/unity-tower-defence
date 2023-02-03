using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySapwner : MonoBehaviour
{
    public EnemyController enemyToSpawn;
    public Transform spawnPoint;
    public float spawnTime = 1f;
    private float spawnCounter;

    public int amountToSpawn = 10;

    public Castle theCastle;

    public Path enemiesInPath;
    public EnemyController enemy;


    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = spawnTime;
        theCastle = FindObjectOfType<Castle>();
        enemiesInPath = FindObjectOfType<Path>();
    }

    // Update is called once per frame
    void Update()
    {
        if (amountToSpawn > 0 && theCastle.currentHealth > 0)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                spawnCounter = spawnTime;
                // spawnTime -= 0.015f;
                enemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
                enemy.GetComponent<Animator>().Play("Base Layer.WalkFWD");
                enemiesInPath.enemiesList.Add(enemy);
                amountToSpawn--;
            }
        }

    }
}
