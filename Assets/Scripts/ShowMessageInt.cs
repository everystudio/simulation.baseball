using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowMessageInt : MonoBehaviour
{
    public TextMeshProUGUI m_txt;
    public string m_strFormat;
    public void Show(int _iValue)
    {
        m_txt.text = string.Format(m_strFormat, _iValue);
    }

}
