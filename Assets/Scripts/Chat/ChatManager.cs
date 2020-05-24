using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class ChatManager : MonoBehaviourPunCallbacks, IChatClientListener
{
    ConnectionProtocol connectProtocol = ConnectionProtocol.Udp;
    ChatClient chatClient;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject broadcastPrefab;

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

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if(messages[0].ToString().StartsWith("/"))
            gameManager.SendMessageToChat($"<color=#FFD700><i><b>{senders[0]}</b> used a secret command</i></color>");
        else
            gameManager.SendMessageToChat($"<b>[ {senders[0]} ]</b> {messages[0]}");
      
        if (messages[0].ToString().StartsWith("/"))
        {
            string[] args = messages[0].ToString().Substring(1).Split(' ');
            
            if(args[0] == "broadcast")
            {
                photonView.RPC("SendBroadcast", RpcTarget.Others, messages[0].ToString().Substring(10));
            }
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
