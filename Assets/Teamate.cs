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

    [SerializeField]
    private Image teamImage;

    public TeamateObject teamateObject;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        if (teamateObject.index != 0)
            transform.position = new Vector2(transform.position.x, transform.position.y - 30 * teamateObject.index);

        // Display playerName
        if (playerName != null)
        {
            playerName.text = teamateObject.player.Value.NickName;
        }

        // Display indexNumber
        if (indexNumber != null)
        {
            indexNumber.text = teamateObject.player.Key.ToString();
        }

        if(teamImage != null)
        {
            if(teamateObject.team == Teams.BLUE)
            {
                teamImage.color = Color.cyan;
            }
            else
            {
                teamImage.color = Color.red;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (teamateObject == null)
        {
            Destroy(this.gameObject);
            return;
        }
    }

    #region Public Methods

    public void SetTarget(TeamateObject teamateObject)
    {
        if (teamateObject == null)
        {
            Debug.LogError("<color=red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
            return;
        }
        // Cache references for efficiency
        this.teamateObject = teamateObject;
    }

    #endregion
}

public class TeamateObject
{
    public KeyValuePair<int, Photon.Realtime.Player> player;
    public int index;
    public Teams team;

    public TeamateObject(KeyValuePair<int, Photon.Realtime.Player> _target, int index, Teams team)
    {
        this.player = _target;
        this.index = index;
        this.team = team;
    }
}
