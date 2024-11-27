using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] GameObject PickupEffect;
    [SerializeField] AudioClip Soundeffect;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
            if(collision.gameObject.tag == "Player")
            {
                GameManager.instance.IncrementCoinCount();
           
                Instantiate(PickupEffect, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(Soundeffect);
                Destroy(this.gameObject,0.2f);
                
            }
            
        
    }
}
