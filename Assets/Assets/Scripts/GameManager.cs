using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManager : VersionedView {

    public enum BoardState { Flipping, Comparing };
    public BoardState currentState = BoardState.Flipping;

    public Sprite[] cardFace;
    public Sprite cardBack;

    public GameObject[] cards;
    private Card card1 = null;
    private Card card2 = null;

    private static GameManager _manager;

    public static GameManager GetGameManager()
    {
        return _manager;
    }
    public void Start()
    {
        InitializeCards();
    }

	public override void DirtyUpdate () {

        if(currentState == BoardState.Comparing)
        {
            Compare();
        }
	}
    void InitializeCards()
    {
        List<int> cardIndexList = new List<int> { };
        List<int> cardValueList = new List<int> { };

        for (int i = 0; i < cards.Length; i++)
        {
            cardIndexList.Add(i);
        }
        
        for (int id = 0; id < 2; id++)
            for (int val = 0; val < 13; val++)
            {
                cardValueList.Add(val);
            }

        List<int> randomizedIndexList = RandomizeList(cardIndexList);
        List<int> randomizedValueList = RandomizeList(cardValueList);

        for(int m = 0; m < cardIndexList.Count; m++)
        {
            cards[m].GetComponent<Card>().InitializeCard(randomizedValueList[m]);
        }

    }
    public Sprite GetCardBack()
    {
        return cardBack;
    }
    public Sprite GetCardFace(int i)
    {
        return cardFace[i];
    }
    private List<int> RandomizeList(List<int> l)
    {
        for(int i = 0; i < l.Count; i++)
        {
            int temp = l[i];
            int randomIndex = Random.Range(i, l.Count);
            l[i] = l[randomIndex];
            l[randomIndex] = temp;
        }
        return l;
    }
    public void SetCardsToMatch(Card c)
    {
        if (card1 == null)
        {
            card1 = c;
            currentState = BoardState.Flipping;
        }
        else if (card2 == null)
        {
            card2 = c;
            currentState = BoardState.Comparing;
        }
        MarkDirty();
    }
    void Compare()
    {
        if (card1.cardValue == card2.cardValue)
        {
            //A match was found
            Debug.Log("Match Found");
        }
        else
        {
            //No match was found
            Debug.Log("No Match");
            StartCoroutine(card1.FlipDown());
            StartCoroutine(card2.FlipDown());
        }
        card1 = card2 = null;
        currentState = BoardState.Flipping;
    }
}
