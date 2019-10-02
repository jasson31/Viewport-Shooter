using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<Enemy>().Die();
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
