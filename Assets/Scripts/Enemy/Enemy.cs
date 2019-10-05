using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform target;
    public bool isDead = false;
    Animator animator;

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        Destroy(gameObject, 2);
    }

    public void Attack()
    {
        PlayerController.inst.StartCoroutine(PlayerController.inst.TurnHitUIOn());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            animator.SetTrigger("Landed");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
