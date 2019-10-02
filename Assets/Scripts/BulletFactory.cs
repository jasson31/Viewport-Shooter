using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : SingletonBehaviour<BulletFactory>
{
    public GameObject bullet;
    float bulletSpeed = 50;

    public GameObject MakeBullet(Vector3 pos, Quaternion rotation, Vector3 direction)
    {
        GameObject newBullet = Instantiate(bullet, pos, rotation);
        newBullet.transform.Find("Bullet (HQ)").GetComponent<Rigidbody>().velocity = direction.normalized * bulletSpeed;
        newBullet.transform.Find("Shell (HQ)").transform.localPosition += new Vector3(0, -0.1f, 0.05f);
        Destroy(newBullet, 5);
        return newBullet;
    }
}
