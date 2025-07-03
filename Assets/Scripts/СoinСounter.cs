using UnityEngine;
using TMPro;

namespace Assets.Scripts
{
    public class CoinCounter : MonoBehaviour
    {

        private int coinCount = 0;
        public TextMeshProUGUI coinText;

        public AudioClip coinSound;
        private AudioSource audioSource;

        void Start()
        {
            UpdateCoinText();
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void AddCoin()
        {
            
            if (PlayerPrefs.HasKey("money"))
            {

                int newVar = PlayerPrefs.GetInt("money");
                newVar += 1;
                PlayerPrefs.SetInt("money", newVar);
            }
            else
            {
                PlayerPrefs.SetInt("money", 1);
            }
            
            Debug.Log(PlayerPrefs.GetInt("money"));
            PlayCoinSound();
        }

        private void UpdateCoinText()
        {
            if (coinText != null)
            {
                coinText.text = "Coins: " + coinCount.ToString();
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