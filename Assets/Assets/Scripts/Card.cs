using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : VersionedView {

    public enum CardState { Flipped, Hidden};
    public CardState currentState = CardState.Hidden;

    public GameObject manager;

    [SerializeField]
    private int _cardValue;

    private Sprite _cardBack;
    private Sprite _cardFace;

    public float delay = 1f;

    public override void DirtyUpdate()
    {
    }

    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    void Start()
    {
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
            FlipUp();
            MarkDirty();
        }
    }
    public void FlipUp()
    {
        if (currentState == CardState.Hidden)
        {
            GetComponent<Image>().sprite = _cardFace;
            currentState = CardState.Flipped;
            manager.GetComponent<GameManager>().SetCardsToMatch(this);
        }
    }
    public void FlipDown()
    {
        if (currentState == CardState.Flipped)
        {
            GetComponent<Image>().sprite = _cardBack;
            currentState = CardState.Hidden;
            MarkDirty();
        }
    }
}
