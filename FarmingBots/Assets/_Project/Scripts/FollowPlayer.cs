using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //Spawn Points Following Player

    Transform player;
    void Start()
    {
        player = FindObjectOfType<Player_Movement>().transform;
    }

    void Update()
    {if (player == null) return;
        transform.position = new Vector2(player.position.x, transform.position.y);
    }
}
