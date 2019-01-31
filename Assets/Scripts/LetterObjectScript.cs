using UnityEngine;
using UnityEngine.EventSystems;

public class LetterObjectScript : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {

    public delegate void ClickAction();
    public event ClickAction MouseDown;
    public event ClickAction MouseUp;
    public event ClickAction MouseExit;
    public event ClickAction MouseEnter;

    public void OnPointerDown(PointerEventData eventData)
    {
        WordHunt.instance.LetterClick((int)pos().x, (int)pos().y, true);

        MouseDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        WordHunt.instance.LetterClick((int)pos().x, (int)pos().y, false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        WordHunt.instance.LetterHover((int)pos().x, (int)pos().y);

        MouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExit();
    }

    Vector2 pos()
    {
        string[] numbers = transform.name.Split('-');
        int x = int.Parse(numbers[0]);
        int y = int.Parse(numbers[1]);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }

}
