using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region Inspector fields
    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType bulletType;
    [SerializeField] private float reloadTime = 0.5f;
    [SerializeField] private Transform firePoint;
    #endregion

    private bool canShoot = true;

    #region Shooting method
    public void MakeShoot(Vector2 directionBullet)
    {
        if (canShoot && directionBullet != Vector2.zero)
        {
            canShoot = false;

            var bullet = ObjectPooler.Instance.GetObject(bulletType);     
           
            bullet.GetComponent<Bullet>().OnCreate(gameObject.tag, firePoint.position, directionBullet.normalized);

            Invoke("Reload", reloadTime);
        }
    }
    #endregion

    private void Reload()
    {
        canShoot = true;
    }
}
