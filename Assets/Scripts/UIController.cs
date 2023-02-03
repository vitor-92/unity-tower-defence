using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    // Start is called before the first frame update
    private void Awake(){
        instance = this;
    }

    public TMP_Text goldText;
    public TMP_Text towerGoldValue;
}
