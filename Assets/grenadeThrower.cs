using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrower : MonoBehaviour
{
    public float throwforce = 40f;
    public GameObject Grenadeprefab;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Transform grenadeStartPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        GameObject grenade = PhotonNetwork.Instantiate("Prefabs/Grenade", grenadeStartPosition.position, grenadeStartPosition.rotation);
        grenade.transform.position = grenadeStartPosition.position;
        grenade.GetComponent<Grenade>().Throw(mainCam);
    }
}
