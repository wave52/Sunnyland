using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionController : MonoBehaviour
{
    public void Death()
    {
        FindObjectOfType<PlayerController>().ScoreCount();
        Destroy(gameObject);
    }
}
