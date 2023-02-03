using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int enemyLevel;
    public float totalHealth;
    private float currentHealth;
    public float moveSpeed = 1f;
    public float attackDamage = 5f;

    private float attackCounter;
    private Castle theCastle;

    private Path thePath;
    private int currentPoint;
    private bool reachedEnd = false;


    [HideInInspector] public Path enemiesInPath;

    public int goldOnDeath;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = totalHealth;
        thePath = FindObjectOfType<Path>();
        theCastle = FindObjectOfType<Castle>();
        enemiesInPath = FindObjectOfType<Path>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (theCastle.currentHealth > 0)
        {
            if (!reachedEnd)
            {
                transform.LookAt(thePath.points[currentPoint]);
                transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) <= 0.01)
                {
                    if (currentPoint < thePath.points.Length - 1)
                    {
                        currentPoint++;
                    }
                    else
                    {
                        reachedEnd = true;
                    }
                }
            }
            else
            {
                theCastle.TakeDamage(attackDamage);
                enemiesInPath.enemiesList.RemoveAt(0);
                Destroy(gameObject);
            }
        }

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemiesInPath.enemiesList.RemoveAt(0);
            player.addGold(goldOnDeath);
            Destroy(gameObject);
        }
    }
}
