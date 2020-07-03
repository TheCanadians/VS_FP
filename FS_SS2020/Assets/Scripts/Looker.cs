using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker : MonoBehaviour
{

    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            Debug.Log("Player is NULL");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(player.position.x, this.transform.position.y, player.position.z);
        transform.LookAt(targetPos);
    }
}
