using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    
    #region Private Fields


    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private Text playerNameText;

    [Tooltip("UI Text to display the number of Alive players")]
    [SerializeField]
    private Text aliveCounter;


    [Tooltip("UI Slider to display Player's Health")]
    [SerializeField]
    private Slider playerHealthSlider;

    private Player target;

    #endregion


    #region MonoBehaviour Callbacks

    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy itself if the target(Player) is null, It's a fail safe when Photon is destroying Instances of a Player over the network
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        // Reflect the Player Health
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = target.Health;
        }

        // Reflect the Player Health
        if (aliveCounter != null)
        {
            aliveCounter.text = PhotonNetwork.PlayerList.Length.ToString();
        }
    }

    #endregion


    #region Public Methods

    public void SetTarget(Player _target)
    {
        if (_target == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        target = _target;
    }

    #endregion

}
