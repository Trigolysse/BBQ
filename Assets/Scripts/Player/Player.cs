using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public HealthBar healthBar;
    private RaycastHit hit;
    public int currentHealth;
    public Canvas playerUI;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }

        currentHealth = 100;
        if (!photonView.IsMine)
        {
            playerUI.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            healthBar.SetHealth(currentHealth);
            if(currentHealth <= 0)
            {
                gameObject.transform.position = new Vector3(100, 20, 100);
                //Destroy(gameObject);
            }
        }

    }


}
