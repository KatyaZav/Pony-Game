using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] IsFull;
    public GameObject[] Slots;
    
    public GameObject inventory;
    private bool inventoryOn;

    void Start()
    {
        inventoryOn = false;
        inventory.SetActive(false);
    }

    public void Chest()
    {
        if (inventoryOn == false)
        {
            inventoryOn = true;
            inventory.SetActive(true);
        }
        else if (inventoryOn == true)
        {
            inventoryOn = false;
            inventory.SetActive(false);
        }
    }
}
