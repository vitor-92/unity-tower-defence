using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    Vector3 offset;
    public string destinationTag = "DropArea";
    private Vector3 defaultPosition;
    private TroopsFields troopsFields;

    void Awake()
    {
        troopsFields = GameObject.Find("TroopsFields").GetComponent<TroopsFields>();
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        defaultPosition = transform.position;
        transform.GetComponent<Collider>().enabled = false;
    }
    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }
    void OnMouseUp()
    {
        var backToPosition = true;
        var rayOrigin = Camera.main.transform.position;
        var RayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, RayDirection, out hitInfo))
        {
            var destinationTroop = hitInfo.transform.GetComponent<TroopsController>();

            if (hitInfo.transform.tag == destinationTag)
            {
                if (destinationTroop.name == transform.GetComponent<TroopsController>().name)
                {
                    if (destinationTroop.troopLevel == transform.GetComponent<TroopsController>().troopLevel)
                    {
                        if (destinationTroop.troopLevel < 5)
                        {
                            Debug.Log("Merge Tower");

                            var troopPos = hitInfo.transform.position;
                            
                            var fieldIndex = this.gameObject.GetComponent<TroopsController>().getFieldIndex();
                            troopsFields.setFreeFieldPosition(fieldIndex);

                            Destroy(this.gameObject);                            

                            var troopLevel = destinationTroop.upgradeTroopLevel();

                            Destroy(hitInfo.transform.Find("TroopBase").gameObject);
                            var troopBase = Instantiate(Resources.Load("Prefabs/Troops/base_" + troopLevel.ToString(), typeof(GameObject)), hitInfo.transform.position, hitInfo.transform.rotation, hitInfo.transform);
                            troopBase.name = "TroopBase";

                            backToPosition = false;
                        }
                        else
                        {
                            Debug.Log("Max Level Achieved");
                        }
                    }
                    else
                    {
                        Debug.Log("Troop Level doesn`t match");
                    }
                }
                else
                {
                    backToPosition = true;
                    Debug.Log("Merge Unvailable, diferent Troops types");
                }
            }
        }
        if (backToPosition)
        {
            transform.position = defaultPosition;
        }
        transform.GetComponent<Collider>().enabled = true;
    }
    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}