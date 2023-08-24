using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoriesController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _burst;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _burst.Play();
            gameObject.SetActive(false);
            GameManager.Memories += 1;
        }
    }
}
