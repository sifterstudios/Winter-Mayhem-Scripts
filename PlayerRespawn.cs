using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerRespawn : MonoBehaviour
{
    private GameObject[] _spawns;
    private Rigidbody _rigidbody;

    void Awake()
    {
        _spawns = GameObject.FindGameObjectsWithTag(TagConstants.Respawn);
        _rigidbody = GetComponent<Rigidbody>();
    }

    void OnRespawn()
    {
        var rnd = new System.Random();
        int index = rnd.Next(_spawns.Length);

        transform.position = _spawns[index].transform.position;
        _rigidbody.velocity = Vector3.zero;

        GameManager.Instance.RestartGame();
        // SoundManager.Instance.StopCheering();
    }
}