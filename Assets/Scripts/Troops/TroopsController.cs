using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsController : MonoBehaviour
{
    // [HideInInspector] 
    public int fieldIndex;
    public float troopDamage;

    public GameObject modelToRotation;
    public int troopLevel = 1;

    public float shootDelay = 1f;
    private float shootCounter;

    private Transform target;

    private Castle theCastle;

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
            modelToRotation.transform.LookAt(target);
            modelToRotation.transform.rotation = Quaternion.Slerp(modelToRotation.transform.rotation, Quaternion.LookRotation(target.position - transform.position), 5f * Time.deltaTime);
        }

        shootCounter -= Time.deltaTime;
        if (shootCounter <= 0 && target != null && theCastle.currentHealth > 0)
        {
            shootCounter = shootDelay;
            if (enemiesInPath.enemiesList[0] != null)
            {
                enemiesInPath.enemiesList[0].TakeDamage(troopDamage);
            }

        }

    }

    public void setFieldIndex(int index){
        fieldIndex = index;
    }

    public int upgradeTroopLevel(){
        troopLevel++;
        return troopLevel;
    }

    public int getFieldIndex(){
        return fieldIndex;
    }
}
