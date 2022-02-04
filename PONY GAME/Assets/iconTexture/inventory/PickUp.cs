using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public int Id;

    public bool[] GetBoolInventory()
    {
        return inventory.IsFull;
    }
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           /*Instantiate(slotButton, inventory.Slots[Id-1].transform);
           Destroy(gameObject);
           */

            for (int i = 0; i < inventory.Slots.Length; i++)
            {
                if (inventory.IsFull[i] == false)
                {
                    inventory.IsFull[i] = true;
                    Instantiate(slotButton, inventory.Slots[i].transform);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
