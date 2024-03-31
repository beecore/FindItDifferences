using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameChallenge : MonoBehaviour
{
    public LayerMask pairItemLayer;
    private int currentLevelIndex;
    public int currentUnlockItem;
    public GameObject correctIconObject;
    public FoundItem foundItem;
    void Start()
    {
        currentUnlockItem = 0;
         currentLevelIndex = PlayerPrefs.GetInt("Challenge");
        Debug.Log("currentLevelIndex "+ currentLevelIndex);
        GameObject levelObj = Instantiate(Resources.Load("Challenges/Challenge" + (currentLevelIndex + 1).ToString(), typeof(GameObject))) as GameObject;
       // AdsControl.Instance.ShowBannerAd();
    }

    // Update is called once per frame
    void Update()
    {
        //if (IsPointerOverUIObject())
          //  return;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            
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
                              // GameObject starEffect = Instantiate(correctIconObject, starPos, Quaternion.identity);
                                //starEffect.GetComponent<StartEffect>().SetTarget(passedItemBar.passedItemList[currentUnlockItem].transform.position);
                                //starEffect.GetComponent<StartEffect>().Fly();
                                SetCorrectSpot(spotItem.spotIndex);
                               currentUnlockItem++;
                    }
                }
                //if (hit.collider.gameObject.tag == "PairItem")
                //{
                //    Debug.Log("Hit: " + hit.collider.gameObject.name);
                //    SpotItem spotItem = hit.collider.GetComponent<SpotItem>();
                //    Debug.Log("Hit spotItem: " + spotItem);
                //    if (!spotItem.isSpotted)
                //    {
                //        Vector3 starPos = new Vector3(spotItem.spotPos.position.x + 50.0f, spotItem.spotPos.position.y + 50.0f, 0);
                //        GameObject starEffect = Instantiate(correctIconObject, starPos, Quaternion.identity);
                //        starEffect.GetComponent<StartEffect>().SetTarget(passedItemBar.passedItemList[currentUnlockItem].transform.position);
                //        starEffect.GetComponent<StartEffect>().Fly();
                //        SetCorrectSpot(spotItem.spotIndex);
                //        currentUnlockItem++;
                //    }

                //    else
                //        SetIncorrectSpot(click2D);
                //}

                //else
                //{

                //    SetIncorrectSpot(click2D);
                //}

            }
        }
#endif
    }
    void SetCorrectSpot(int spotIndex)
    {
        Debug.Log("Right: ");
        //AudioManager.instance.correctBtn.Play();
        //gameLevel.pairItemTrue.itemList[spotIndex - 1].SetSpotItem();
        //gameLevel.pairItemFalse.itemList[spotIndex - 1].SetSpotItem();
        //passedItemBar.UnlockItem(currentUnlockItem);

        //Debug.Log("Unloc : " + currentUnlockItem);
        //if (currentUnlockItem == gameLevel.pairItemTrue.itemList.Length - 1 && currentState != GAME_STATE.GAME_OVER)
        //{
        //    mUIManager.ShowGameWin();
        //    currentState = GAME_STATE.GAME_WIN;
        //    passedItemBar.GameWinEffect();
        //}

    }
}
