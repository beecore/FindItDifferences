using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PassedItemBar : MonoBehaviour
{
    public GameObject passedItemObj;

    public Transform itemRoot;

    public List<Image> passedItemList = new List<Image>();

    public Sprite lockIcon;

    public Sprite unlockIcon;


    public void InitBar()
    {
        passedItemList.Add(passedItemObj.GetComponent<Image>());
    }

    public void UnlockItem(int itemIndex)
    {
        if(itemIndex < passedItemList.Count)
        {
            

            StartCoroutine(UnlockItemEffect(itemIndex));
        }
           
    }

    IEnumerator UnlockItemEffect(int itemIndex)
    {
        yield return new WaitForSeconds(0.95f);
        passedItemList[itemIndex].sprite = unlockIcon;
        Sequence s = DOTween.Sequence();
        s.Append(passedItemList[itemIndex].transform.DOScale(1.25f, 0.1f).SetEase(Ease.OutBack));
        s.Append(passedItemList[itemIndex].DOColor(Color.yellow, 0.1f).SetEase(Ease.OutBack));

        s.OnComplete(() =>
        {
            passedItemList[itemIndex].transform.DOScale(1.0f, 0.1f).SetEase(Ease.OutBack);
            passedItemList[itemIndex].DOColor(Color.white, 0.1f).SetEase(Ease.OutBack);
        });
    }

    public void GenItem(int itemCount)
    {
       for(int i = 0; i < itemCount - 1; i++)
        {
            GameObject item = Instantiate(passedItemObj, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(itemRoot);
            item.transform.localScale = new Vector3(1, 1, 1);
            passedItemList.Add(item.GetComponent<Image>());

        }
    }

    public void GameWinEffect()
    {
        StartCoroutine(GameWinEffectIE());
      
    }

    public IEnumerator GameWinEffectIE()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < passedItemList.Count; i++)
        {
          
            StartCoroutine(UnlockItemEffect(i));
            yield return new WaitForSeconds(0.25f);
        }

    }

    /*
    Sequence s = DOTween.Sequence();
    s.Append(settingGroup.DOMove(new Vector3(0, -paddingSettingSlide , 0), 0.5f).SetRelative().SetEase(Ease.OutCubic));
        isSlidingSetting = true;
        s.OnComplete(() =>
         {
             isSlidingSetting = false;
             currentSettingState = SETTING_STATE.SHOW;
         });
    */
}
