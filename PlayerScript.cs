using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public Inventory playerInventory;
    [SerializeField] GameObject Weapon,testweapon;
    bool attackFlag;

    public enum State
    {
        Stop,
        Check,
        Dec,
        Play,
    }
    void Start()
    {
        attackFlag = true;
       testweapon = Instantiate(Weapon, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        playerInventory.AddItemToCharbyName("Gun");
        EquipPlayerWeapon(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            playerInventory.listCharItems();
        CharAttack();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            Destroy(collision.gameObject);
            //when mouse position raycasted on enemy area if enemy range hit true? : select little area close to enemyrange
            //in between enemyrange and char then if clicked: set destination to area
            Debug.Log("Entered enemy range");
        }
        if (collision.gameObject.tag == "Item")
        {
            Destroy(collision.gameObject);
            playerInventory.AddItemToCharbyName(collision.gameObject.name);
        }
    }
   

    public void EquipPlayerWeapon(int id)
    {
        Vector3 weaponpos = new Vector3(GameObject.Find("weaponholder").GetComponent<Transform>().position.x,
            GameObject.Find("weaponholder").GetComponent<Transform>().position.y, GameObject.Find("weaponholder").GetComponent<Transform>().position.z); ;
        testweapon.transform.parent = this.transform;
        testweapon.transform.position = weaponpos;
        testweapon.transform.rotation = Quaternion.Euler(-90, 0, 0);

        if (testweapon.transform.parent == this.transform)
        {
            Debug.Log(playerInventory.CheckItemID(id).Name
                +"Weapon attached to object");
        }
    }

    void CharAttack()
    {
        if (attackFlag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                attackFlag = false;
                testweapon.transform.localRotation = Quaternion.Euler(0,0 , 90);
                testweapon.transform.position = new Vector3(GameObject.Find("weaponholder").GetComponent<Transform>().position.x + 0.75f,
                    GameObject.Find("weaponholder").GetComponent<Transform>().position.y - 0.5f, GameObject.Find("weaponholder").GetComponent<Transform>().position.z);
                Debug.Log("Entered here");
                StartCoroutine(Waitforsec());
            }
        }
    }

    IEnumerator Waitforsec()
    {
        print(Time.time);
        yield return new WaitForSeconds(0.2f);

        Vector3 weaponpos = new Vector3(GameObject.Find("weaponholder").GetComponent<Transform>().position.x,
            GameObject.Find("weaponholder").GetComponent<Transform>().position.y, GameObject.Find("weaponholder").GetComponent<Transform>().position.z);
        testweapon.transform.position = weaponpos;
        testweapon.transform.localRotation = Quaternion.Euler(-90, 0, 90);
        attackFlag = true;
    }
}