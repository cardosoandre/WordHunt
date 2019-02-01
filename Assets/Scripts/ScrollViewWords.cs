using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollViewWords : MonoBehaviour {

    private RectTransform rect;

    public static ScrollViewWords instance;
    public GameObject wordCellPrefab;
    public Transform scrollViewContent;

    private void Awake()
    {
        instance = this;

        rect = GetComponent<RectTransform>();

        WordHunt wh = WordHunt.instance;

        rect.sizeDelta = new Vector2(rect.sizeDelta.x, (wh.cellSize.y * wh.gridSize.y + wh.cellSpacing.y));
    }

    public void SpawnWordCell(string word,float delay)
    {
        GameObject cell = Instantiate(wordCellPrefab, scrollViewContent);
        cell.GetComponentInChildren<Text>().text = word.ToUpper();
        cell.transform.DOScale(0, 0.3f).SetEase(Ease.OutSine).From().SetDelay(delay);
    }

    public void CheckWord(string word)
    {
        for (int i = 0; i < scrollViewContent.childCount; i++)
        {
            Text t = scrollViewContent.GetChild(i).GetComponentInChildren<Text>();

            if (t.text.ToLower() == word || t.text.ToLower() == WordHunt.Reverse(word))
            {
                scrollViewContent.GetChild(i).GetComponent<Image>().color = Color.green;
            }
        }
    }
}
