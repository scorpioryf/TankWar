using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject explosionPrefab;

    public GameObject player;

    public Sprite deadEagle;
    public AudioClip DieAudio;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }



    public void Die()
    {
        sr.sprite = deadEagle;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerManager.Instance.lifeValue = 0;
        PlayerManager.Instance.GameOver = true;
        AudioSource.PlayClipAtPoint(DieAudio, transform.position);
    }
}
