using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowBaseCondition : MonoBehaviour
{
    public Image[] m_imgBaseArr;
    public void Show(List<int> _runnner)
    {
        for (int i = 0; i < m_imgBaseArr.Length; i++)
        {
            m_imgBaseArr[i].color = _runnner.Contains(i) ? Color.red : Color.white;
        }
    }
}
