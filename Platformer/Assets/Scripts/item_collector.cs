using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_collector : MonoBehaviour
{
    public static int coins;

    [SerializeField] private Text coinsText;

    [SerializeField] private AudioSource coinSFX;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Coin")) {
            Destroy(collision.gameObject);
            coinSFX.Play();
            coins++;  
            coinsText.text = "Coins x" + coins;
        }
    }
}    