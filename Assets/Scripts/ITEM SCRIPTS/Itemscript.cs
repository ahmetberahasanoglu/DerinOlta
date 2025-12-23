using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{

    public float hook_Speed;

    public int scoreValue;

    void OnDisable()
    {
        if (GameplayManager.instance == null)
        return;

    GameplayManager.instance.DisplayScore(scoreValue);
    }

}
