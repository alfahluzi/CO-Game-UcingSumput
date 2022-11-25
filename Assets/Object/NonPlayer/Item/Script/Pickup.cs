using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private PlayerInventori inventori;
    private PlayerCollectionInventory collectionInventory;
    public GameObject imageItem;
    public string type;
    public string itemName;
    // Start is called before the first frame update
    void Start()
    {
        inventori = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventori>();
        collectionInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCollectionInventory>();
        imageItem.GetComponent<SpriteRenderer>().sortingLayerName = "Background";
        imageItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == "COLLECTION")
            {
                // image item set sorting layer dan sorting order
                imageItem.GetComponent<SpriteRenderer>().sortingLayerName = "UI Front";
                imageItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
                imageItem.transform.localScale = new Vector3(200f, 200f, 200f);
                // simpan data item berupa nama dan obj gambar
                collectionInventory.AddItem(itemName, imageItem);
                // destroy obj
                Destroy(gameObject);

            }
            else if (type == "BUFF")
            {
                for (int i = 0; i < inventori.items.Length; i++)
                {
                    if (inventori.isFull[i] == false)
                    {
                        inventori.isFull[i] = true;
                        inventori.items[i] = itemName;
                        imageItem.GetComponent<SpriteRenderer>().sortingLayerName = "UI Front";
                        imageItem.GetComponent<SpriteRenderer>().sortingOrder = 2;
                        imageItem.transform.localScale = new Vector3(20f, 20f, 20f);
                        Instantiate(imageItem, inventori.slotItem[i].transform, false);
                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
    }
}
