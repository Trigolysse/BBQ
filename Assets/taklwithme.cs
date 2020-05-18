using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class taklwithme : MonoBehaviour
{
    public Dialogue dialogue;
    public GameObject talk;
    private bool begin = false;
    public GameObject dialogmanager;

    private void OnTriggerEnter(Collider other)
    {
        talk.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (begin==false)
            {
                talk.SetActive(false);
                            
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                begin = true;
            }
            else
            {
                Debug.Log("Vive Kaaris");
                dialogmanager.GetComponent<DialogueManager>().DisplayNextSentence();
                
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        talk.SetActive(false);
        begin = false;
        dialogmanager.GetComponent<DialogueManager>().EndDialogue();
    }
}
