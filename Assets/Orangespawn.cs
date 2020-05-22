using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using Photon.Pun;

public class Orangespawn : MonoBehaviour
{
    public GameObject flower;
    public List<Vector3[]> allspwan = new List<Vector3[]>();
    public Vector3[] spawn1;
    public Vector3[] spawn2;
    public float respawntime;
    public List<bool> alive;
    public List<float> restime;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
            spawnFlowers();
    }
    void spawnFlowers()
    {
        Random rand = new Random();
        allspwan.Add(spawn1);
        allspwan.Add(spawn2);
        int i = 0;
        foreach (Vector3[] ranpos in allspwan)
        {
            GameObject newflower = PhotonNetwork.InstantiateSceneObject("OrangeFlower", ranpos[rand.Next(ranpos.Length)], Quaternion.identity) as GameObject;
            newflower.GetComponent<Orangeloot>().zone = i;
            alive.Add(true);
            restime.Add(0);
            i += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Random rand = new Random();
        for (int i = 0; i < alive.Count; i++)
        {
            if (!alive[i])
            {
                restime[i] += Time.deltaTime;
            }
            if (restime[i] >= respawntime)
            {
                restime[i] = 0;
                alive[i] = true;
                if (PhotonNetwork.IsMasterClient)
                {
                    GameObject newflower = PhotonNetwork.InstantiateSceneObject("OrangeFlower", allspwan[i][rand.Next(allspwan[i].Length)], Quaternion.identity) as GameObject;
                    newflower.GetComponent<Orangeloot>().zone = i;
                }
            }
        }
    }
    public void destroyflower(int zone)
    {
        alive[zone] = false;
    }
}
