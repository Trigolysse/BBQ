﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton; //button used for creating and joining a game.
    [SerializeField]
    private GameObject quickCancelButton; //button used to stop searing for a game to join.
    [SerializeField]
    private int RoomSize; //Manual set the number of player in the room at one time.
    [SerializeField]
    private GameObject roomSlotObject;

    private GameObject canvas;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var room in roomList) {
            Debug.Log(room);
        }

        base.OnRoomListUpdate(roomList);
    }

    private void UpdateRoomListUI()
    {
        GameObject newRoomSlot = Instantiate(roomSlotObject, this.transform);
        //newRoomSlot.textObject = newText.GetComponent<Text>();
        //newRoomSlot.textObject.text = newMessage.text;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        //canvas.GetComponent<Image>().color = new Color32(255, 0, 0, 255); 
    }

    public override void OnConnectedToMaster() //Callback function for when the first connection is established successfully.
    {
        //canvas.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
        PhotonNetwork.AutomaticallySyncScene = true; //Makes it so whatever scene the master client has loaded is the scene all other clients will load
        quickStartButton.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public void QuickStart() //Paired to the Quick Start button
    {
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); //First tries to join an existing room
    }

    public override void OnJoinRandomFailed(short returnCode, string message) //Callback function for if we fail to join a rooom
    {
        Debug.Log("Failed to join a room");
        CreateRoom();
    }

    void CreateRoom() //trying to create our own room
    {
        Debug.Log("Creating room now");
        int randomRoomNumber = Random.Range(0, 10000); //creating a random name for the room
        RoomOptions roomOps = new RoomOptions() {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)RoomSize,
            CleanupCacheOnLeave = false
        };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps); //attempting to create a new room
    }
    public override void OnCreateRoomFailed(short returnCode, string message) //callback function for if we fail to create a room. Most likely fail because room name was taken.
    {
        Debug.Log("Failed to create room... trying again");
        CreateRoom(); //Retrying to create a new room with a different name.
    }
    public void QuickCancel() //Paired to the cancel button. Used to stop looking for a room to join.
    {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}