using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeThrower : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private Transform grenadeStartPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && GetComponent<SimpleInv>().Bigitem[1])
        {
            ThrowGrenade();
            GetComponent<SimpleInv>().Bigitem[1] = false;
            GetComponent<SimpleInv>().allimage[1].color = Color.black;
            GetComponent<BuySell>().Ui[1].color = Color.white;
        }
    }

    void ThrowGrenade()
    {
        if (!photonView.IsMine)
            return;
        GameObject grenade = PhotonNetwork.Instantiate("Prefabs/Grenade", grenadeStartPosition.position, grenadeStartPosition.rotation);
        grenade.transform.position = grenadeStartPosition.position;
        grenade.GetComponent<Grenade>().Throw(mainCam);
        grenade.GetComponent<Grenade>().source = photonView.Owner.NickName;
    }
}
