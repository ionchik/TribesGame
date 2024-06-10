using UnityEngine;
using UnityEngine.UI;

public class PrairieView : MonoBehaviour
{
    [SerializeField] private Prairie _prairie;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject[] _holders;

    private void Start()
    {
        _prairie.CardsChanged.AddListener(OnCardsChanged);
    }

    private void OnDestroy()
    {
        _prairie.CardsChanged.RemoveListener(OnCardsChanged);
    }

    private void OnCardsChanged(int cardsNumber)
    {
        for ( int index = 0; index < _holders.Length; index++)
        {
            _holders[index].SetActive(cardsNumber <= index);
        }
        _scrollRect.enabled = cardsNumber > _holders.Length;
    }
}
