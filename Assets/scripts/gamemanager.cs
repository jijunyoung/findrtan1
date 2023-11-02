using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEditor.Build.Content;

public class gamemanager : MonoBehaviour
{
    public Text timetxt;
    public GameObject card;
    float time;
    public static gamemanager I;
    public GameObject firstcard;
    public GameObject secondcard;
    public GameObject endtext;

    void Awake()
    {
        I = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i <16; i++)
        {
            

            GameObject newcard = Instantiate(card);
            newcard.transform.parent = GameObject.Find("cards").transform;
            float x = (i % 4) * 1.4f - 2.1f;
            float y = (i / 4) * 1.4f - 3.0f;
            newcard.transform.position = new Vector3(x, y, 0);
            
            
            string rtanname = "rtan" + rtans[i].ToString();
            newcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanname);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timetxt.text = time.ToString("n2");
        if (time > 30.0f)
        {
            endtext.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }

    public void ismatched()
    {
        string firstCardImage = firstcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondcard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            
            firstcard.GetComponent<card>().destroyCard();
            secondcard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;
            if (cardsLeft == 2)
            {
                endtext.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            
            firstcard.GetComponent<card>().closeCard();
            secondcard.GetComponent<card>().closeCard();
        }

        firstcard = null;
        secondcard = null;
    }
  
    
        
    
}
