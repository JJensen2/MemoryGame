using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : VersionedView {

    public enum CardState { Flipped, Hidden};
    public CardState currentState = CardState.Hidden;

    public GameObject manager;

    private int _cardValue;

    private Sprite _cardBack;
    private Sprite _cardFace;

    public float delay = 1f;

    public override void DirtyUpdate()
    {
        //can be used if flip animations are added
    }

    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public void InitializeCard(int initValue)
    {
        _cardValue = initValue;
        _cardBack = manager.GetComponent<GameManager>().GetCardBack();
        _cardFace = manager.GetComponent<GameManager>().GetCardFace(_cardValue);
    }
    public void ClickedOn()
    {
        if (currentState == CardState.Hidden)
        {
            CardFlip();
        }
    }
    public void CardFlip()
    {
        if (currentState == CardState.Hidden)
            FlipUp();
        else
            StartCoroutine(FlipDown());
    }
    public void FlipUp()
    {
        GetComponent<Image>().sprite = _cardFace;
        currentState = CardState.Flipped;
        manager.GetComponent<GameManager>().SetCardsToMatch(this);
        MarkDirty();
    }
    public IEnumerator FlipDown()
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Image>().sprite = _cardBack;
        currentState = CardState.Hidden;
        MarkDirty();
    }
}
