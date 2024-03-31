using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RewardManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PileOfCoinParent;
    [SerializeField]
    private TextMeshProUGUI counter;
    private Vector3[] InitialPos;
    private Quaternion[] InitialRotation;

    // Start is called before the first frame update
    void Start()
    {
        InitialPos = new Vector3[10];
        InitialRotation = new Quaternion[10];

        for(int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            InitialPos[i] = PileOfCoinParent.transform.GetChild(i).position;
            InitialRotation[i] = PileOfCoinParent.transform.GetChild(i).rotation;
        }
    }

    public void Reset()
    {
        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            PileOfCoinParent.transform.GetChild(i).position = InitialPos[i];
            PileOfCoinParent.transform.GetChild(i).rotation = InitialRotation[i];
        }
    }

    public void RewardPileOfCoin(int noCoin)
    {
        Reset();

        var delay = 0f;

        PileOfCoinParent.SetActive(true);

        for (int i = 0; i < PileOfCoinParent.transform.childCount; i++)
        {
            PileOfCoinParent.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

            PileOfCoinParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(384, 1124), 1f).SetDelay(delay + 0.5f);
            PileOfCoinParent.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);

            PileOfCoinParent.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.8f).SetEase(Ease.OutBack).OnComplete(CountCoinByComplete);

            delay += 0.1f;
        }

        StartCoroutine(CountCoins(10));
    }

    void CountCoinByComplete()
    {
        PlayerPrefs.SetInt("CountCoin", PlayerPrefs.GetInt("CountCoin") + 1);
        counter.text = PlayerPrefs.GetInt("CountCoin").ToString();
    }

    IEnumerator CountCoins(int coinNo)
    {
        yield return new WaitForSecondsRealtime(1f);

        var timer = 0f;

        for(int i = 0; i < coinNo; i++)
        {
            PlayerPrefs.SetInt("CountCoin", PlayerPrefs.GetInt("CountCoin") + 1);

            counter.text = PlayerPrefs.GetInt("CountCoin").ToString();
            timer += 0.05f;
            yield return new WaitForSecondsRealtime(timer);
        }
    }
}
