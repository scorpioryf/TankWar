using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : MonoBehaviour
{
    public AudioClip hitAudio;
    public float moveSpeed = 10;

    //public bool isPlayersBullet = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                //if (!isPlayersBullet)
                //{
                //    collision.SendMessage("Die");
                //}
                break;

            case "Eagle":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;

            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;

            case "Barrier":
                Destroy(gameObject);
                AudioSource.PlayClipAtPoint(hitAudio, transform.position);
                break;

            case "Water":
                break;

            case "Enemy":
                //if (isPlayersBullet)
                //{
                collision.SendMessage("Die");
                Destroy(gameObject);
                //}
                break;

            case "Grass":
                break;

            default:
                break;
        }
    }

}
