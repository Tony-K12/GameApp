using UnityEngine;
using UnityEngine.EventSystems;

public class Coin : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CoinTapGameManager.Instance.AddScore(1);
        CoinPooler.Instance.ReturnCoin(gameObject);
        AudioManager.Instance.PlaySFX("coin");
    }
}
