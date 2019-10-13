using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public ScoreManager scoreManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            GetComponent<Collider>().enabled = false;
            scoreManager.SetResultUI();
            GameManager.inst.GameOver();
        }
    }
}
