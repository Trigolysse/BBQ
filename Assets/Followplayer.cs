using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followplayer : MonoBehaviour
{
  public float distancedujoueur;
  public float recularriere;
  private Vector3 Initialposition;

  private Vector3 positionplayer;
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        positionplayer = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = new Vector3(positionplayer.x, positionplayer.y + distancedujoueur, positionplayer.z+recularriere);
    }
}
