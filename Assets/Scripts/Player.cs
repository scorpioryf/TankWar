using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static int Xaxis = 1;

    private static int Yaxis = -1;

    private static int Naxis = 0;

    private Vector3 bulletEulerAngles;

    //private int towards = Naxis;

    //private float keyStatus = 0;

    public float moveSpeed = 3;

    private SpriteRenderer sr;

    public Sprite[] tankSprite;// up  right down left

    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject shieldOnPrefab;

    public AudioSource moveAudio;
    public AudioClip[] engineAudio;

    public AudioClip fire;

    private float defendTimeVal = 3;

    private class Status
    {
        public int towards = Naxis;
        public float keyStatus = 0;
        public float h = 0;
        public float v = 0;
        public bool isDefended = true;
    }

    Status s = new Status();

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
        if (s.isDefended)
        {
            shieldOnPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                s.isDefended = false;
                shieldOnPrefab.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (PlayerManager.Instance.GameOver)
        {
            s.isDefended = false;
            Die();
        }
    }
    
    private void FixedUpdate()
    {
        Move();
        
    }

    private void Attack()
    {
        AudioSource.PlayClipAtPoint(fire, transform.position);
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
        
    }

    private void Move()
    {

        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        s.v = v;
        s.h = h;
        float currentKeyStatus = h + v;
        if(v != 0 || h != 0)
        {
            moveAudio.clip = engineAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
            if(currentKeyStatus == s.keyStatus)
            {
                switch (s.towards)
                {

                    case 1:
                        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

                        if (h < 0)
                        {
                            sr.sprite = tankSprite[3];
                            bulletEulerAngles = new Vector3(0, 0, 90);
                        }
                        else if (h > 0)
                        {
                            sr.sprite = tankSprite[1];
                            bulletEulerAngles = new Vector3(0, 0, -90);
                        }
                        break;

                    case -1:
                        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

                        if (v < 0)
                        {
                            sr.sprite = tankSprite[2];
                            bulletEulerAngles = new Vector3(0, 0, 180);
                        }
                        else if (v > 0)
                        {
                            sr.sprite = tankSprite[0];
                            bulletEulerAngles = new Vector3(0, 0, 0);
                        }
                        break;
                    
                }
            }
            else
            {
                switch(s.towards)
                {
                    case 0:
                        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
                        if (h < 0)
                        {
                            sr.sprite = tankSprite[3];
                            bulletEulerAngles = new Vector3(0, 0, 90);
                            s.towards = Xaxis;
                            break;
                        }
                        else if (h > 0)
                        {
                            sr.sprite = tankSprite[1];
                            bulletEulerAngles = new Vector3(0, 0, -90);
                            s.towards = Xaxis;
                            break;
                        }

                        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
                        if (v < 0)
                        {
                            sr.sprite = tankSprite[2];
                            bulletEulerAngles = new Vector3(0, 0, 180);
                            s.towards = Yaxis;
                            break;
                        }
                        else if (v > 0)
                        {
                            sr.sprite = tankSprite[0];
                            bulletEulerAngles = new Vector3(0, 0, 0);
                            s.towards = Yaxis;
                            break;
                        }
                        break;

                    case 1:
                            transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);

                            if (v < 0)
                            {
                                sr.sprite = tankSprite[2];
                            bulletEulerAngles = new Vector3(0, 0, 180);
                            s.towards = Yaxis;
                            }
                            else if (v > 0)
                            {
                                sr.sprite = tankSprite[0];
                            bulletEulerAngles = new Vector3(0, 0, 0);
                            s.towards = Yaxis;
                            }
                        break;

                    case -1:
                        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

                        if (h < 0)
                        {
                            sr.sprite = tankSprite[3];
                            bulletEulerAngles = new Vector3(0, 0, 90);
                            s.towards = Xaxis;
                        }
                        else if (h > 0)
                        {
                            sr.sprite = tankSprite[1];
                            bulletEulerAngles = new Vector3(0, 0, -90);
                            s.towards = Xaxis;
                        }
                        break;
                }
            }
        }
        else
        {
            moveAudio.clip = engineAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
            s.towards = Naxis;
        }

        s.keyStatus = currentKeyStatus;
    }

    //private int Move(int towards)
    //{

    //    float v = Input.GetAxisRaw("Vertical");
    //    float h = Input.GetAxisRaw("Horizontal");


    //    switch (towards)
    //    {
    //        case 1:
    //            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
    //            if (h < 0)
    //            {
    //                sr.sprite = tankSprite[3];
    //                return towards;
    //            }
    //            else if (h > 0)
    //            {
    //                sr.sprite = tankSprite[1];
    //                return towards;
    //            }

    //            transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
    //            if (v < 0)
    //            {
    //                sr.sprite = tankSprite[2];
    //                towards = Yaxis;
    //                return towards;
    //            }
    //            else if (v > 0)
    //            {
    //                sr.sprite = tankSprite[0];
    //                towards = Yaxis;
    //                return towards;
    //            }

    //            towards = Naxis;
    //            break;

    //        case -1:
    //            transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
    //            if (v < 0)
    //            {
    //                sr.sprite = tankSprite[2];
    //                return towards;
    //            }
    //            else if (v > 0)
    //            {
    //                sr.sprite = tankSprite[0];
    //                return towards;
    //            }

    //            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
    //            if (h < 0)
    //            {
    //                sr.sprite = tankSprite[3];
    //                towards = Xaxis;
    //                return towards;
    //            }
    //            else if (h > 0){
    //                sr.sprite = tankSprite[1];
    //                towards = Xaxis;
    //                return towards;
    //            }

    //            towards = Naxis;
    //            break;

    //        case 0:
    //            transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
    //            if (v < 0)
    //            {
    //                sr.sprite = tankSprite[2];
    //                towards = Yaxis;
    //                return towards;
    //            }
    //            else if (v > 0)
    //            {
    //                sr.sprite = tankSprite[0];
    //                towards = Yaxis;
    //                return towards;
    //            }
    //            transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
    //            if (h < 0)
    //            {
    //                sr.sprite = tankSprite[3];
    //                towards = Xaxis;
    //                return towards;
    //            }
    //            else if (h > 0)
    //            {
    //                sr.sprite = tankSprite[1];
    //                towards = Xaxis;
    //                return towards;
    //            }
    //            towards = Naxis;
    //            break;
    //    }

    //    return towards;

    //}

    private void Die()
    {
        if (s.isDefended)
        {
            return;
        }
        //调用爆炸动画
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //玩家死亡
        Destroy(gameObject);
        PlayerManager.Instance.isDead = true;
    }
}
