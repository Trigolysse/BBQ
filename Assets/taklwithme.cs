﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class taklwithme : MonoBehaviourPunCallbacks
{
    private int next = 0;
    public Dialogue dialogue;
    public GameObject talk;
    private bool begin = false;
    public GameObject dialogmanager;

    private void OnTriggerEnter(Collider other)
    {
        if (Vector3.Distance(other.transform.position,transform.position)<3)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().OnTalk();
            }
            talk.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
       
             if (next>0)
             {
                        next--;
             }
            
             if (!begin && Input.GetKeyUp(KeyCode.O))
             {
                 if (other.CompareTag("Player"))
                 {
                     other.gameObject.GetComponent<Player>().NoTalk();
                 }
                 talk.SetActive(false);
                 next = -1;
             }
             if (Input.GetKeyUp(KeyCode.F) && next==0)
             {
                        if (begin==false)
                        {
                            talk.SetActive(false);
                            if (other.CompareTag("Player"))
                            {
                                other.gameObject.GetComponent<Player>().NoTalk();
                            }
                                        
                            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                            begin = true;
                            next = 25;
                        }
                        else
                        {
                            Debug.Log("Vive Kaaris");
                            dialogmanager.GetComponent<DialogueManager>().DisplayNextSentence();
                            next = 25;
            
                        }
                        
             }
        
    }

    private void OnTriggerExit(Collider other)
    {
       
            next = 0;
            talk.SetActive(false);
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().NoTalk();
            }
            begin = false;
            dialogmanager.GetComponent<DialogueManager>().EndDialogue();
        
        
    }
}
