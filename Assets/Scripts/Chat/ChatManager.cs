﻿using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using UnityEngine;



public class ChatManager : MonoBehaviourPunCallbacks, IChatClientListener
{
    ConnectionProtocol connectProtocol = ConnectionProtocol.Udp;
    ChatClient chatClient;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject broadcastPrefab;
    [SerializeField]
    private CoreEngine engine;


    void Start()
    {
        AuthenticationValues authValues = new AuthenticationValues();
        authValues.UserId = PlayerPrefs.GetString(Prefs.PLAYER_NAME_PREF);
        authValues.AuthType = CustomAuthenticationType.None;
        chatClient = new ChatClient(this, connectProtocol);
        chatClient.ChatRegion = "EU";
        chatClient.Connect("c212f995-7a94-43d4-9b01-067a8b58d7af", "0.01", authValues);
    }

    void Update()
    {
        if (chatClient != null) { chatClient.Service(); }
    }

    public void sendMessage(string message)
    {
        chatClient.PublishMessage("channelNameHere", message);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
      
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnConnected()
    {
        chatClient.Subscribe(new string[] { "channelNameHere" }); //subscribe to chat channel once connected to server
        chatClient.PublishMessage("channelNameHere", "<color=red>[ Server ]</color> User joined");
    }

    public void OnDisconnected()
    {
        
    }
    private GameObject GetPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PhotonView>().IsMine)
            {
                return player;
            }
        }
        return null;
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if(!messages[0].ToString().StartsWith("/"))
            gameManager.SendMessageToChat($"<b>[ {senders[0]} ]</b> {messages[0]}");
        else
        {
            string[] args = messages[0].ToString().Substring(1).Split(' ');
            switch (args[0])
            {
                case "broadcast":
                    photonView.RPC("SendBroadcast", RpcTarget.Others, messages[0].ToString().Substring(10));
                    gameManager.SendMessageToChat($"<color=#FFD700><i><b>{senders[0]}</b> used a secret command</i></color>");
                    break;
                case "tp":
                    Tp(senders[0], args[1]);
                    gameManager.SendMessageToChat($"<color=#FFD700><i><b>{senders[0]}</b> used a secret command</i></color>");
                    break;
                case "start":
                    if(args.Length > 1)
                        GameObject.Find("CoreEngine").GetComponent<PhotonView>().RPC("StartNewGame", RpcTarget.All,(float) int.Parse(args[1]));
                    else
                        GameObject.Find("CoreEngine").GetComponent<PhotonView>().RPC("StartNewGame", RpcTarget.All);
                    break;
                case "pause":
                    if (engine != null)
                        engine.gameState = GameState.PAUSED;
                    break;
                case "kick":
                    PhotonNetwork.CloseConnection(PhotonNetwork.PlayerList[(int)Random.Range(0, PhotonNetwork.PlayerList.Length-1)]);
                    break;
                case "money":
                    if (args.Length > 1)
                        GetPlayer().GetComponent<SimpleInv>().money += int.Parse(args[1]);
                    else
                        chatClient.PublishMessage("channelNameHere", "<color=red>[ Server ]</color> You need to specify the amount");
                    break;
                case "team":
                    if (args.Length > 1)
                    {
                        if (args[1].ToLower() == "blue")
                        {
                            GetPlayer().GetComponent<Player>().team = Teams.BLUE;
                            GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>().SetTeamIndicator(true);
                        }    
                        if (args[1].ToLower() == "red")
                        {
                            GetPlayer().GetComponent<Player>().team = Teams.RED;
                            GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<PlayerUI>().SetTeamIndicator(true);
                        }     
                    } 
                    else
                        chatClient.PublishMessage("channelNameHere", "<color=red>[ Server ]</color> You need to specify a team [red | blue]");
                    break;
                default:
                    gameManager.SendMessageToChat($"<color=green><i><b>{senders[0]}</b> is autistic</i></color>");
                    break;
            }
        }
    }

    private void Tp(string sender, string other)
    {
        GameObject senderPlayer = GameObject.Find(sender);
        GameObject otherPlayer = GameObject.Find(other);
        if(otherPlayer != null && senderPlayer != null)
        {
            senderPlayer.transform.position = otherPlayer.transform.position;
        }
    }

    [PunRPC]
    public void SendBroadcast(string message)
    {
        if(broadcastPrefab != null)
        {
            GameObject _uiGo = Instantiate(broadcastPrefab);
            _uiGo.SendMessage("SetMessage", message, SendMessageOptions.RequireReceiver);
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed to a new channel!");
    }

    public void OnUnsubscribed(string[] channels)
    {
      
    }

    public void OnUserSubscribed(string channel, string user)
    {
      
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
       
    }
}
