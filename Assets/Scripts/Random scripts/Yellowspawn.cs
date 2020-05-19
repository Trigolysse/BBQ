using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Photon.Pun;

public class Yellowspawn : MonoBehaviour
{
    public GameObject flower;
    public List<Vector3[]> allspwan = new List<Vector3[]>();
    public int size;
    public Vector3[] spawn1;
    public Vector3[] spawn2;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("prout");
        Random rand = new Random();
        allspwan.Add(spawn1);
        allspwan.Add(spawn2);
        foreach(Vector3[] ranpos in allspwan)
        {
            GameObject newflower = PhotonNetwork.Instantiate("YellowFlower", ranpos[rand.Next(size)], Quaternion.identity) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
