using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class FermeBoutique : MonoBehaviour
{
    
    private Player player;
    private Tir tir;
    private PlayerMovement playermovement;
    private PlayerSetup playersetup;
    public GameObject remove;
    public UnityEngine.UI.Button destroy;
    public GameObject trueplayer;

    // Start is called before the first frame update
    void Start()
    {
        player = trueplayer.GetComponent<Player>();
        tir = trueplayer.GetComponent<Tir>();
        playermovement = trueplayer.GetComponent<PlayerMovement>();
        playersetup = trueplayer.GetComponent<PlayerSetup>();
        destroy.onClick.AddListener ((UnityEngine.Events.UnityAction)this.OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
        player.isOutOfFocus = false;
        remove.SetActive(false);



    }
    
}
