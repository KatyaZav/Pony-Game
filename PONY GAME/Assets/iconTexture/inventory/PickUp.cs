using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject slotButton;
    public int Id;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
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
