using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectionInventory : MonoBehaviour
{
    private string lup = "LUP";
    private string onigiri = "ONIGIRI";
    private string botol = "BOTOL";

    public List<ItemCollection> slotItems;
    public GameObject UIInventory;
    public Transform itemCollectionTransform;
    Player_BuffEffect buffEffect;
    public class ItemCollection
    {
        public GameObject image;
        public string name;
    }
    private void Start()
    {
        slotItems = new List<ItemCollection>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            OpenUIInventory();
        }
    }

    public void AddItem(string _name, GameObject _image)
    {
        ItemCollection newItem = new ItemCollection();
        newItem.name = _name;
        newItem.image = _image;

        slotItems.Add(newItem);
        GameObject newPosition = new GameObject();
        newPosition.transform.position = itemCollectionTransform.position;
        newPosition.transform.position += new Vector3(0, 0.75f + (0.25f * slotItems.Count - 1), 0);
        Instantiate(_image, newPosition.transform.position, newPosition.transform.rotation, itemCollectionTransform);
        Destroy(newPosition);
    }

    public void OpenUIInventory()
    {
        UIInventory.SetActive(!UIInventory.activeInHierarchy);
    }


}
