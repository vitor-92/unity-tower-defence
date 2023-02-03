using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : MonoBehaviour
{
    public float towerDamage;
    public int towerLevel = 1;
    public GameObject projectile;
    public Transform firePoint;
    public float shotTime = 1f;
    private float shootCounter;

    private Transform target;

    private Castle theCastle;

    public Transform launcherModel;

    private Path enemiesInPath;

    void Start()
    {
        theCastle = FindObjectOfType<Castle>();
        enemiesInPath = FindObjectOfType<Path>();
    }

    void Update()
    {
        if (enemiesInPath.enemiesList.Count > 0)
        {
            target = enemiesInPath.enemiesList[0].transform;
        }
        else
        {
            target = null;
        }

        if (target != null)
        {
            launcherModel.LookAt(target);
            launcherModel.rotation = Quaternion.Slerp(launcherModel.rotation, Quaternion.LookRotation(target.position - transform.position), 5f * Time.deltaTime);
        }

        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0 && target != null && theCastle.currentHealth > 0)
        {
            shootCounter = shotTime;
            firePoint.LookAt(target);
            if (enemiesInPath.enemiesList[0] != null)
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                enemiesInPath.enemiesList[0].TakeDamage(towerDamage);
            }

        }
        
    }
}
