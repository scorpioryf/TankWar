using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3;
    private Vector3 bullectEulerAngles;
    private SpriteRenderer sr;
    public Sprite[] tankSprite;

    public GameObject explosionPrefab;
    public GameObject bulletPrefab;

    public AudioClip fire;

    private float timeVal;
    private float directionTimeVal = 1;

    float v = -1;
    float h;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeVal >= 3)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Die()
    {
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        Destroy(gameObject);
        PlayerManager.Instance.playerScore++;
    }


    private void Move()
    {
        if (directionTimeVal >= 2)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                h = -1;
                v = 0;
            }
            else if (num > 2 && num <= 5)
            {
                h = 1;
                v = 0;
            }
            directionTimeVal = 0;
        }
        else
        {
            directionTimeVal += Time.fixedDeltaTime;
        }
        


        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bullectEulerAngles = new Vector3(0, 0, -180);
        }

        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bullectEulerAngles = new Vector3(0, 0, 0);
        }

        //if (Mathf.Abs(v) > 0.05f)
        //{
        //    moveAudio.clip = tankAudio[1];

        //    if (!moveAudio.isPlaying)
        //    {
        //        moveAudio.Play();
        //    }
        //}

        if (v != 0)
        {
            return;
        }

        
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bullectEulerAngles = new Vector3(0, 0, 90);
        }

        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bullectEulerAngles = new Vector3(0, 0, -90);
        }

        //if (Mathf.Abs(h) > 0.05f)
        //{
        //    moveAudio.clip = tankAudio[1];

        //    if (!moveAudio.isPlaying)
        //    {
        //        moveAudio.Play();
        //    }
        //}
        //else
        //{
        //    moveAudio.clip = tankAudio[0];

        //    if (!moveAudio.isPlaying)
        //    {
        //        moveAudio.Play();
        //    }
        //}
    }

    private void Attack()
    {
        AudioSource.PlayClipAtPoint(fire, transform.position);
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bullectEulerAngles));
        timeVal = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            directionTimeVal = 4;
        }
    }
}
