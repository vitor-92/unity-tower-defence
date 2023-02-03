using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int currentGold = 0;
    private TroopsFields towers;

    void Start(){
        towers = FindObjectOfType<TroopsFields>();
        UIController.instance.goldText.text = currentGold.ToString();
        UIController.instance.towerGoldValue.text = towers.goldCost.ToString();
    }

    public void addGold(int goldAmout){
        currentGold += goldAmout;
        UIController.instance.goldText.text = currentGold.ToString();
    }

    public void removeGold(){
        currentGold -= towers.goldCost;
        UIController.instance.goldText.text = currentGold.ToString();
    }
}
