using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LayerMask pairItemLayer;

    public GameLevel gameLevel;

    public int currentLevelIndex;

    public int maxLevelTotal;

    public GameObject missIconObject;

    public GameObject correctIconObject;

    public GameObject hintObj;

    public PassedItemBar passedItemBar;

    public int currentUnlockItem;

    public float currentTimer;

    public UIManager mUIManager;

    public static GameManager instance;

    public enum GAME_STATE
    {
        GAME_PLAYING,
        GAME_PAUSE,
        GAME_OVER,
        GAME_WIN
    };

    public GAME_STATE currentState;

    public bool testMode;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Bat dau");
        GameObject levelObj = null;

        if (!testMode)
        {
            currentLevelIndex = PlayerPrefs.GetInt("Level");

            if (currentLevelIndex >= maxLevelTotal)
            {
                currentLevelIndex = 0;
            }
            levelObj = Instantiate(Resources.Load("Levels/Level" + (currentLevelIndex + 1).ToString(), typeof(GameObject))) as GameObject;
        }

        if (levelObj != null)
        {
            gameLevel = levelObj.GetComponent<GameLevel>();
        }
        currentUnlockItem = 0;
        passedItemBar.InitBar();
        passedItemBar.GenItem(gameLevel.pairItemTrue.itemList.Length);
        currentTimer = gameLevel.levelTimer;
        currentState = GAME_STATE.GAME_PLAYING;
        AdsControl.Instance.ShowBannerAd();
        int isOpenHint = PlayerPrefs.GetInt("isOpenHint", 0);

        if (currentLevelIndex == 0 && isOpenHint == 0)
        {
            hintObj.SetActive(true);
            PlayerPrefs.SetInt("isOpenHint", 1);
        }

        if (currentLevelIndex == 0)
        {
            hintObj.transform.position = gameLevel.pairItemTrue.itemList[0].showSpot.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsPointerOverUIObject())
            return;

        if (currentState != GAME_STATE.GAME_PLAYING)
            return;

        if (currentTimer - Time.deltaTime >= 0)
        {
            currentTimer -= Time.deltaTime;
            mUIManager.mGamePanel.ShowTimer(currentTimer);
        }
        else
        {
            currentTimer = 0.0f;
            mUIManager.mGamePanel.ShowTimer(currentTimer);
            currentState = GAME_STATE.GAME_OVER;
            mUIManager.ShowGameOver();
        }

#if !UNITY_EDITOR

 if(Input.touchCount > 0)
        {
            Touch theTouch = Input.GetTouch(0);

            if(theTouch.phase == TouchPhase.Began)
            {
                hintObj.SetActive(false);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero, 1.0f, pairItemLayer);
                Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector3 click2D = new Vector3(clickpos.x, clickpos.y, 0.0f);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "PairItem")
                    {
                        // Debug.Log("Hit: " + hit.collider.gameObject.name);
                        SpotItem spotItem = hit.collider.GetComponent<SpotItem>();
                        if (!spotItem.isSpotted)
                        {
                            Vector3 starPos = new Vector3(spotItem.spotPos.position.x + 50.0f, spotItem.spotPos.position.y + 50.0f, 0);
                            GameObject starEffect = Instantiate(correctIconObject, starPos, Quaternion.identity);
                            starEffect.GetComponent<StartEffect>().SetTarget(passedItemBar.passedItemList[currentUnlockItem].transform.position);
                            starEffect.GetComponent<StartEffect>().Fly();
                            SetCorrectSpot(spotItem.spotIndex);
                            currentUnlockItem++;
                        }
                        else
                            SetIncorrectSpot(click2D);
                    }
                    else
                    {
                        SetIncorrectSpot(click2D);
                    }
                }
            }
        }

#endif

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            hintObj.SetActive(false);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1.0f, pairItemLayer);
            Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 click2D = new Vector3(clickpos.x, clickpos.y, 0.0f);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "PairItem")
                {
                    Debug.Log("Hit: " + hit.collider.gameObject.name);
                    SpotItem spotItem = hit.collider.GetComponent<SpotItem>();
                    Debug.Log("Hit spotItem: " + spotItem);
                    if (!spotItem.isSpotted)
                    {
                        Vector3 starPos = new Vector3(spotItem.spotPos.position.x + 50.0f, spotItem.spotPos.position.y + 50.0f, 0);
                        GameObject starEffect = Instantiate(correctIconObject, starPos, Quaternion.identity);
                        starEffect.GetComponent<StartEffect>().SetTarget(passedItemBar.passedItemList[currentUnlockItem].transform.position);
                        starEffect.GetComponent<StartEffect>().Fly();
                        SetCorrectSpot(spotItem.spotIndex);
                        currentUnlockItem++;
                    }
                    else
                        SetIncorrectSpot(click2D);
                }
                else
                {
                    SetIncorrectSpot(click2D);
                }
            }
        }
#endif
    }

    private void SetCorrectSpot(int spotIndex)
    {
        AudioManager.instance.correctBtn.Play();
        gameLevel.pairItemTrue.itemList[spotIndex - 1].SetSpotItem();
        gameLevel.pairItemFalse.itemList[spotIndex - 1].SetSpotItem();
        passedItemBar.UnlockItem(currentUnlockItem);

        Debug.Log("Unloc : " + currentUnlockItem);
        if (currentUnlockItem == gameLevel.pairItemTrue.itemList.Length - 1 && currentState != GAME_STATE.GAME_OVER)
        {
            mUIManager.ShowGameWin();
            currentState = GAME_STATE.GAME_WIN;
            passedItemBar.GameWinEffect();
        }
    }

    private void SetIncorrectSpot(Vector3 clickPos)
    {
        AudioManager.instance.failBtn.Play();
        if (currentTimer - 30 > 0)
        {
            currentTimer -= 30;
            mUIManager.mGamePanel.ShowMinusTimer();
            Instantiate(missIconObject, clickPos, Quaternion.identity);
        }
        else
        {
            currentTimer = 0;
            mUIManager.mGamePanel.ShowTimer(currentTimer);
            currentState = GAME_STATE.GAME_OVER;
            mUIManager.ShowGameOver();
        }
    }

    public void ShowHint()
    {
        AdsControl.Instance.ShowRewardedAd(AdsControl.REWARD_TYPE.HINT);
    }

    public void ShowHintRW()
    {
        AudioManager.instance.clickBtn.Play();
        if (FindHintIndex() != -1)
        {
            hintObj.SetActive(true);
            hintObj.transform.position = gameLevel.pairItemTrue.itemList[FindHintIndex()].showSpot.position;
        }
    }

    public int FindHintIndex()
    {
        int hintIndex = -1;

        for (int i = 0; i < gameLevel.pairItemTrue.itemList.Length; i++)
        {
            if (!gameLevel.pairItemTrue.itemList[i].isSpotted)
                hintIndex = i;
        }

        return hintIndex;
    }

    public void Replay()
    {
        AudioManager.instance.clickBtn.Play();
        SceneManager.LoadScene("Game");
    }

    public void Back()
    {
        AudioManager.instance.clickBtn.Play();
        SceneManager.LoadScene("Home");
    }

    public void ContinueGame()
    {
        AdsControl.Instance.ShowRewardedAd(AdsControl.REWARD_TYPE.MORE_TIME);
    }

    public void ShowContinueRW()
    {
        currentTimer = 60;
        currentState = GAME_STATE.GAME_PLAYING;
        mUIManager.HideGameOver();
    }

    public bool IsPointerOverUIObject()
    {
        bool _check = false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<Button>() != null)
                _check = true;
        }
        results.Clear();
        return _check;
        //return results.Count > 0;
    }
}