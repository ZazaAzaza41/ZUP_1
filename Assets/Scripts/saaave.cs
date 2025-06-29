using UnityEngine;

[System.Serializable]
public class saaave : MonoBehaviour
{

    //private int money;
    //private Vector3 _lastPosition;
    void Start()
    {
        
    }
    /*public void save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("Playerinfo", json);
    }
    public void Load()
    {
        string json = PlayerPrefs.GetString("Playerinfo");
        JsonUtility.FromJsonOverwrite(json, this);

        //transform.position = _lastPosition;
    }*/
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //money = 0;
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

        }
        
            

    }
}
