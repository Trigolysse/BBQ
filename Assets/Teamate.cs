using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teamate : MonoBehaviour
{
    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Text indexNumber;

    private KeyValuePair<int, Photon.Realtime.Player> player;
    int index=0;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        if (index != 0)
            transform.position = new Vector2(transform.position.x, transform.position.y - 30 * index);
        Debug.Log(player.Key);

        // Display playerName
        if (playerName != null)
        {
            playerName.text = player.Value.NickName;
        }

        // Display indexNumber
        if (indexNumber != null)
        {
            indexNumber.text = player.Key.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Public Methods

    public void SetTarget(KeyValuePair<int, Photon.Realtime.Player> _target)
    {
        if (_target.Equals(default(KeyValuePair<int, Photon.Realtime.Player>)))
        {
            Debug.LogError("<color=red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        player = _target;
    }

    public void SetIndex(int _index)
    {
        // Cache references for efficiency
        index = _index;
    }

    #endregion
}
