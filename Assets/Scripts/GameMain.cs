using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public Text m_txtResult;
    public void OnQuickResult()
    {
        int iParam = Random.Range(0, 100);
        if( iParam < 50)
        {
            m_txtResult.text = "Ÿ‚¿";
        }
        else
        {
            m_txtResult.text = "•‰‚¯";
        }
    }
}
