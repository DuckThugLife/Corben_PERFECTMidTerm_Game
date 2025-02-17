using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager endingLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endingLevel.EndLevel();
            Destroy(gameObject);
        }
    }

}
