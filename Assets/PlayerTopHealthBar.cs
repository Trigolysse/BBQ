using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTopHealthBar : MonoBehaviour
{
    Player player;

    [SerializeField]
    private Slider slider;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.Health;
    }
}
