using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;

    private void Start()
    {
        inventory = GameObject.FindGameObjectsWithTag("Player").GetComponent<Inventory>();
    }

}
