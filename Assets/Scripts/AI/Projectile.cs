using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 direction = new Vector3();
    [SerializeField] GameObject myFX;
    bool appliedDamage;
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] AudioClip hitSound;
    AudioSource enemyAudio;

    private void Awake()
    {
        enemyAudio = GameObject.Find("Enemy Audio Source").GetComponent<AudioSource>();
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
    [SerializeField] float timeToDie = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
        timeToDie -= Time.deltaTime;
        if(timeToDie < 0)
        {
            Instantiate(myFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Tank")
        {
            if (!appliedDamage) collision.gameObject.GetComponent<TankScript>().reduceHealth();
            appliedDamage = true;
            Instantiate(myFX, transform.position, Quaternion.identity);
            enemyAudio.PlayOneShot(hitSound, Random.Range(0.1f, 0.25f));
            Destroy(gameObject);
        }
        if (collision.gameObject.name.Contains("Wall"))
        {
            Instantiate(myFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
