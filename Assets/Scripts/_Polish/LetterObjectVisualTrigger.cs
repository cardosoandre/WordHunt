using UnityEngine;
using DG.Tweening;

public class LetterObjectVisualTrigger : MonoBehaviour {

    private LetterObjectScript letterObject;

	void Awake () {

        letterObject = GetComponent<LetterObjectScript>();

        letterObject.MouseDown += MouseDown;
        letterObject.MouseUp += MouseUp;
        letterObject.MouseEnter += MouseEnter;
        letterObject.MouseExit += MouseExit;

    }

    private void Start()
    {
        transform.DOScale(0, .5f).SetEase(Ease.OutBack).From();
    }

    public void MouseDown()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.highlight, 1 + (WordHunt.instance.highlightedObjects.Count * 0.2f));
    }

    public void MouseUp()
    {

    }

    public void MouseEnter()
    {
        if(WordHunt.instance.activated && WordHunt.instance.highlightedObjects.Contains(transform)){
            AudioManager.instance.PlaySound(AudioManager.instance.highlight, 1 + (WordHunt.instance.highlightedObjects.Count * 0.2f));
        }else{
            AudioManager.instance.PlaySound(AudioManager.instance.select, 1);
        }


        transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
    }

    public void MouseExit()
    {
        transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
    }


    private void OnDestroy()
    {
        
    }
}
