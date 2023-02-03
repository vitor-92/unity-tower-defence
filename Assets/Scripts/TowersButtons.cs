using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersButtons : MonoBehaviour
{
    // private TowersGrid towersgrid;
    private TroopsFields troopsFields;
    private List<bool> towersBusyList;
    void Start()
    {
        troopsFields = FindObjectOfType<TroopsFields>();
    }
    public void addTowerToGrid()
    {
        troopsFields.placeTower();
    }
}
