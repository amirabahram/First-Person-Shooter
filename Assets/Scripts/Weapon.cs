using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletPrefabLifeTime = 3f;
    Animator anim;
    public int totalBulletCount = 100;
    public int slutBulletCount = 10;
    bool isReloading;
    public float reloadAmmoLeftTime = 2f;
    public float reloadOutOfAmmoTime = 2.5f;


    // Start is called before the first frame update
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public bool GetReloadingState() {
        return isReloading;
    }
    public Animator GetAnim()
    {
        return anim;
    } 
    private void FireWeapon()
    {
        anim.Play("Armature|fire");
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position,Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized*bulletVelocity,ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefabLifeTime));
        slutBulletCount -= 1;
        if (slutBulletCount == 0 && !isReloading) ReloadOutOfAmmoFlag();

    }
    private void ReloadAmmoLeftFlag()
    {
        isReloading = true;
        anim.Play("Armature|reload_ammo_left");
        Invoke("ReloadAmmoLeft", reloadAmmoLeftTime);

    }
    private void ReloadOutOfAmmoFlag()
    {
        isReloading = true;
        anim.Play("Armature|reload_out_of_ammo");
        Invoke("ReloadOutOfAmmo", reloadOutOfAmmoTime);
    }
    private void ReloadAmmoLeft()
    {
        int temp = 10 - slutBulletCount;
        slutBulletCount = 10;
        totalBulletCount -= temp;
        isReloading = false;
    }
    private void ReloadOutOfAmmo()
    {
        slutBulletCount = 10;
        totalBulletCount -= 10;
        isReloading = false;
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet ,float bulletPrefabLifeTime)
    {
        yield return new WaitForSeconds(bulletPrefabLifeTime);
        Destroy(bullet);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(slutBulletCount > 0 && !isReloading) FireWeapon();

        }
        if(Input.GetKeyDown(KeyCode.R) && !isReloading) ReloadAmmoLeftFlag();
        

    }
}
