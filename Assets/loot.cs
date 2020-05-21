using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class loot : MonoBehaviour
{

    [SerializeField]
    private Sprite flowerSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("FEFEW");
        if(other.CompareTag("Player"))
        {
            Debug.Log(other.gameObject);
            var playerInventory = other.gameObject.GetComponent<Inventory>();
            if(playerInventory != null)
                playerInventory.AddInInventory(new Stack(new Item(0, "flower", flowerSprite, ItemType.DIRT), 1));
            PhotonNetwork.Destroy(this.gameObject);
        }
       
       
    }
}
