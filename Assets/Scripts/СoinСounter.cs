using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class CoinCounter : MonoBehaviour
    {

        private int coinCount = 0;
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI coinRecord;

        public AudioClip coinSound;
        private AudioSource audioSource;

        void Start()
        {          
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void AddCoin()
        {
            coinCount++;

            if (PlayerPrefs.HasKey("money"))
            {
                if (coinCount > PlayerPrefs.GetInt("money"))
                {
                    PlayerPrefs.SetInt("money", coinCount);
                }             
            }
            else
            {
                PlayerPrefs.SetInt("money", coinCount);
            }
            PlayCoinSound();
            UpdateCoinText();
            UpdateCoinRecordText();
        }

        private void UpdateCoinText()
        {
            if (coinText != null)
            {
                coinText.text = "Coins: " + coinCount.ToString();
            }
        }

        private void UpdateCoinRecordText()
        {
            if (coinText != null)
            {
                coinRecord.text = "SCORE: " + PlayerPrefs.GetInt("money").ToString();
            }
        }

        private void PlayCoinSound()
        {
            if (coinSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(coinSound);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                
                AddCoin();
                Destroy(other.gameObject);
            }
        }
    }
}