using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchDroitRange : MonoBehaviour
{
    public GameObject PunchDroit;
    public GameObject Attakpoint;
    public float punchrange;
    public LayerMask PlayerLayer;
    public LayerMask MonsterLayer;
    // Start is called before the first frame update
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Attakpoint.transform.position,punchrange);
    }
}
