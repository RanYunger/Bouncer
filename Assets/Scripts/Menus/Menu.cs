using UnityEngine;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{
    // Methods
    public void OnClick()
    {
        AudioManager.instance.Play("Click");
    }
    public void OnPointerEnter(Button button)
    {
        button.GetComponent<Image>().color = Color.gray;

        AudioManager.instance.Play("Swipe");
    }
    public void OnPointerExit(Button button)
    {
        button.GetComponent<Image>().color = Color.white;
    }
}