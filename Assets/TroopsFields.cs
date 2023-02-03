using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsFields : MonoBehaviour
{
    public GameObject[] fields;
    public GameObject[] troops;
    public bool[] busy;
    private List<int> availableSpots;
    private Player player;
    public int goldCost = 10;
    public string pathToBaseLvl1 = "Prefabs/Troops/base_1";

    void Awake()
    {
        busy = new bool[15];
        availableSpots = new List<int>();
    }
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            busy[i] = false;
        }

        player = FindObjectOfType<Player>();
    }

    public void placeTower()
    {
        if (player.currentGold >= goldCost)
        {
            var freeFieldIndex = getAvailableSpotPosition();
            if (freeFieldIndex >= 0)
            {
                player.removeGold();
                addGoldCost();
                var i = Random.Range(0, troops.Length);

                var troop = Instantiate(troops[i], fields[freeFieldIndex].transform.position - troops[i].transform.position, fields[freeFieldIndex].transform.rotation);
                troop.GetComponent<TroopsController>().setFieldIndex(freeFieldIndex);

                var troopBase = Instantiate(Resources.Load(pathToBaseLvl1, typeof(GameObject)), troop.transform.position, troop.transform.rotation, troop.transform);
                troopBase.name = "TroopBase";
                busy[freeFieldIndex] = true;
            }
            else
            {
                Debug.Log("No spots available");
            }
        }
    }

    public void addGoldCost()
    {
        goldCost += 10;
        UIController.instance.towerGoldValue.text = goldCost.ToString();
    }

    int getAvailableSpotPosition()
    {
        availableSpots.Clear();
        for (int i = 0; i < 15; i++)
        {
            if (!busy[i])
            {
                availableSpots.Add(i);
            }
        }
        if (availableSpots.Count > 0)
        {
            var index = Random.Range(0, availableSpots.Count - 1);
            return availableSpots[index];
        }
        return -1;
    }

    public void setFreeFieldPosition(int index){
        busy[index] = false;
    }
}
