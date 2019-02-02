using UnityEngine;
using UnityEngine.UI.Extensions;
using DG.Tweening;

public class HighlightBehaviour : MonoBehaviour {

    public static HighlightBehaviour instance;

    public GameObject lineRendererPrefab;

    public Color[] colors;
    public int colorCounter;

    private void Awake()
    {
        instance = this;
        WordHunt.FoundWord += SetLineRenderer;
    }

    void SetLineRenderer(RectTransform t1, RectTransform t2){

        GameObject line = Instantiate(lineRendererPrefab, transform);

        line.GetComponent<UILineRenderer>().color = colors[colorCounter];
        colorCounter = (colorCounter == colors.Length - 1) ? 0 : colorCounter + 1;

        line.transform.DOScale(0,0.3f).From().SetEase(Ease.OutBack);

        RectTransform[] points = new RectTransform[2];
        points.SetValue(t1,0);
        points.SetValue(t2,1);

        line.GetComponent<UILineConnector>().transforms = points;


    }

    private void OnDestroy()
    {
        WordHunt.FoundWord -= SetLineRenderer;
    }
}
