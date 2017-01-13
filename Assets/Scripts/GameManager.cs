using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {

    public Sprite[] cardFace;
    public Sprite cardBack;
    public GameObject[] cards;
    public Text matchText;

    private bool _init = false;
    private int _matchCount = 13;

	
	// Update is called once per frame
	void Update () {
        if (!_init)
            InitializeCards();
        if (Input.GetMouseButtonUp(0))
            CheckCards();
	}
    void InitializeCards()
    {
        for (int id = 0; id < 2; id++)
            for (int i = 0; i < 2; i++)
            {
                bool test = false;
                int choice = 0;
                while (!test)
                {
                    choice = Random.Range(0, cards.Length);
                    test = !(cards[choice]).GetComponent<Card>().initialized;
                }
                cards[choice].GetComponent<Card>().cardValue = i;
                cards[choice].GetComponent<Card>().initialized = true;
            }
        foreach (GameObject c in cards)
            c.GetComponent<Card>().SetCardGraphics();
        if (!_init)
            _init = true;
    }
    public Sprite GetCardBack()
    {
        return cardBack;
    }
    public Sprite GetCardFace(int i)
    {
        return cardFace[i-1];
    }
    void CheckCards()
    {
        List<int> l = new List<int>();

        for (int i = 0; i < cards.Length; i++)
            if (cards[i].GetComponent<Card>().faceUp == true)
                l.Add(i);
    if(l.Count == 2)
        MatchCheck(c);
    }
    void MatchCheck(List<int> c)
    {
        Card.allowFlipping = true;

        int x = 0;

        if(cards[c[0]].GetComponent<Card>().cardValue == cards[c[1]].GetComponent<Card>().cardValue)
        {
            x = 2;
            _matchCount--;
            matchText.text = "Remaining Matches: " + _matchCount;
            if (_matchCount == 0)
                SceneManager.LoadScene("Menu");
        }

        for (int i = 0; i < c.Count; i++)
        {
            cards[c[i]].GetComponent<Card>().faceUp = true;
            cards[c[i]].GetComponent<Card>().FalseCheck();
        }
    }
}
