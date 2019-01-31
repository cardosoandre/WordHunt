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
        transform.DORotate(Vector3.forward * 90, .5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutBack).From();
    }

    public void MouseDown()
    {

    }

    public void MouseUp()
    {

    }

    public void MouseEnter()
    {
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
