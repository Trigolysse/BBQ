using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Photon.Pun;

public class Yellowspawn : MonoBehaviourPunCallbacks
{
    public GameObject flower;
    public List<Vector3[]> allspwan = new List<Vector3[]>();
    public int size;
    public Vector3[] spawn1;
    public Vector3[] spawn2;
    public Vector3[] spawn3;
    public Vector3[] spawn4;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("prout");
       
        if(photonView.IsMine)
        {
            spawnFlowers();
            photonView.RPC("spawnFlowers", RpcTarget.All);
        }
        
    }

    [PunRPC]
    void spawnFlowers()
    {
        Random rand = new Random();
        allspwan.Add(spawn1);
        allspwan.Add(spawn2);
        allspwan.Add(spawn3);
        allspwan.Add(spawn4);

        foreach (Vector3[] ranpos in allspwan)
        {
            GameObject newflower = PhotonNetwork.Instantiate("YellowFlower", ranpos[rand.Next(size)], Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
