using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Inventory playerInventory;
    [SerializeField] GameObject Weapon,testweapon;

    public enum State
    {
        Stop,
        Check,
        Dec,
        Play,
    }
    void Start()
    {
       testweapon = Instantiate(Weapon, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        playerInventory.AddItemToCharbyName("Sword");
        EquipPlayerWeapon(1);
        //playerInventory.AddItemToCharbyName("Lantern");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            playerInventory.AddItemToCharbyName(collision.gameObject.name);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            playerInventory.listCharItems();
    }

    public void EquipPlayerWeapon(int id)
    {
        Vector3 weaponpos = new Vector3(this.transform.position.x+ 0.75f,
            this.transform.position.y + 0.5f, this.transform.position.z-0.5f);
        testweapon.transform.parent = this.transform;
        testweapon.transform.position = weaponpos;

        if (testweapon.transform.parent == this.transform)
        {
            Debug.Log(playerInventory.CheckItemID(id).Name.Length
                +"Weapon attached to object");
        }
    }
}