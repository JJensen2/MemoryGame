using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour {

    public static bool allowFlipping = false;

    [SerializeField]
    private bool _faceUp;
    [SerializeField]
    private int _cardValue;
    [SerializeField]
    private bool _initialized = false;

    private Sprite _cardBack;
    private Sprite _cardFace;

    private GameObject _manager;

    void Start()
    {
        _faceUp = true;
    }
    public void SetCardGraphics()
    {
        _cardBack = _manager.GetComponent<GameManager>().getCardBack();
        _cardFace = _manager.GetComponent<GameManager>().getCardFace(cardValue);

        FlipCard();
    }

    void FlipCard()
    {
        if (_faceUp && allowFlipping)
            GetComponent<Image>().sprite = _cardBack;
        else if (!_faceUp && allowFlipping)
            GetComponent<Image>().sprite = _cardFace;
    }
    public int cardValue
    {
        get { return _cardValue; }
        set { _cardValue = value; }
    }

    public bool faceUp
    {
        get { return _faceUp; }
        set { _faceUp = value; }
    }

    public bool initialized
    {
        get { return _initialized; }
        set { _initialized = value; }
    }
    public GameObject manager
    {
        get { return _manager; }
        set { _manager = value; }
    }

    public void FalseCheck()
    {
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        if(_faceUp == true)
            GetComponent<Image>().sprite = _cardBack;
        else
            GetComponent<Image>().sprite = _cardFace;
        allowFlipping = false;
    }
}
