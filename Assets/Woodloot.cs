using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Woodloot : MonoBehaviour
{
    public int zone;
    private GameObject gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.FindWithTag("Gamemanager");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gamemanager.GetComponent<Woodspawn>().destroyflower(zone);
            //var playerInventory = other.gameObject.GetComponent<Inventory>();
            //if(playerInventory != null)
            //{
              //  playerInventory.AddInInventory(new Stack(Items.getItemWithType(ItemType.WOOD), 1));
            //}
                
            PhotonNetwork.Destroy(this.gameObject);
            other.GetComponent<SimpleInv>().Add(Loot.Wood, 1);
        }
    }
}
