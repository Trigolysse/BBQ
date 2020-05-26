using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{

    #region Public Fields

    static public GameManager Instance;
    public GameObject chatPanel, textObject;
    public InputField chatInputField;
    public ChatManager chatManager;
    public GameObject SkipIntro;
    public GameObject ChatMenu;
    public GameObject EscapeMenu;
    public Camera Camer;
    public Text text;
    public Canvas DeathCanvas;
    public delegate void OnPlayerKilledCallback(string killer, string victim, WeaponName weaponName);
    public OnPlayerKilledCallback onPlayerKilledCallback;
    public int nextPlayerTeam;
    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;

    #endregion

    #region Private Fields

    private float Frames;
    private int frames;
    private bool create = false;
    public GameObject chatMenu;
    public GameObject playerController;
    private bool isShowing;
    [SerializeField]
    private List<Message> messageList = new List<Message>();
    [SerializeField]
    private int maxMessages;

    #endregion

    public void UpdateTeam()
    {
        if (nextPlayerTeam==1)
        {
            nextPlayerTeam = 2;
        }
        else
        {
            nextPlayerTeam = 1;
        }
    }

    private GameObject GetPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject player in players)
        {
            if(player.GetComponent<PhotonView>().IsMine)
            {
                return player;
            }
        }
        return null;
    }


    public void setMenu(bool b)
    {
        
    }

    #region MonoBehaviour CallBacks
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.LoadLevel(0);
        Debug.Log(nextPlayerTeam);
        Instance = this;
        SkipIntro.SetActive(true);
        EscapeMenu.SetActive(false);
        ChatMenu.SetActive(false);
    }

    private void Update()
    {
        
        Frames=Time.time;
        if (create==false && Input.GetKeyDown(KeyCode.Space) || create==false && Frames>23f)
        {
            create = true;
            CreatePlayer();
            SkipIntro.SetActive(false);
            ChatMenu.SetActive(true);
            EscapeMenu.SetActive(true);
            Destroy(Camer);
        }

        if (create)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {             
                isShowing = !isShowing;
                GameObject player = GetPlayer();
                if (isShowing)
                    player.GetComponent<Player>().isOutOfFocus = true;
                else
                    player.GetComponent<Player>().isOutOfFocus = false;

                chatInputField.gameObject.SetActive(isShowing);
                chatInputField.Select();
                chatInputField.ActivateInputField();


                if (!isShowing && chatInputField.text != "")
                {
                    chatManager.sendMessage(chatInputField.text);
                    chatInputField.text = "";
                }
            }
        }
        else
        {
            frames++;
            if (frames>80)
            {
                frames = 1;
            }

            if (frames>40)
            {
                text.enabled = false;
            }
            else
            {
                text.enabled = true;
            }
        }
        
        
    }

    #endregion

    #region Public Methods


    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    #endregion

    private void CreatePlayer()
    {
        Debug.Log("Creating Player");
        PhotonNetwork.Instantiate(Path.Combine("Prefabs", Tags.PLAYER_TAG), new Vector3(0,13,0), Quaternion.identity);
    }

    public void quitGame()
    {
        Debug.Log("Quiting Game");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }

    public void SendMessageToChat(string text)
    {
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].textObject.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = Instantiate(textObject, chatPanel.transform);
        newMessage.textObject = newText.GetComponent<Text>();
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
        
    }
   
}

[System.Serializable]
public class Message
{
    public string text;
    public Text textObject;
}