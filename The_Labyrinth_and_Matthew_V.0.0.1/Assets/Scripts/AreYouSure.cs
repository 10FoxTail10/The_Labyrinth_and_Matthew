using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreYouSure : MonoBehaviour
{
    public int count;
    public Text text;

    private void Start()
    {
        count = 0;
        text.text = count.ToString();
    }

    public void ButClick()
    {
        count++;
        text.text = count.ToString();
        Debug.Log(count);
    }

    private void Update()
    {
        if (count > 10 && count < 15)
        {
            text.text = "Ты не выйдешь от сюда)";
        }
        else if (count > 20 && count < 35)
        {
            text.text = "Не старайся, скрипт не работает(";
        }
        else if (count > 100 && count < 105)
        {
            text.text = "Ладно, если ты так хочешь выйти, то нажми ещё пару раз)";
        }
        else if (count > 105)
        {
            Application.Quit();
        }
    }
}
