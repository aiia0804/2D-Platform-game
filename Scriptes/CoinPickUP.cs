using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUP : MonoBehaviour
{
    [SerializeField] AudioClip CoinSoundsSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != collision.GetComponent<CapsuleCollider2D>()) { return; }
        var target = collision.GetComponent<Player>();
        if (target)
        {
            Destroy(gameObject);
            FindObjectOfType<GameSession>().AddToCoint();
            AudioSource.PlayClipAtPoint(CoinSoundsSFX, Camera.main.transform.position);
        }
    }
}
