using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMain : MonoBehaviour
{
    public Text m_txtResult;
    public void OnQuickResult()
    {
        int iParam = Random.Range(0, 100);
        if (iParam < 50)
        {
            m_txtResult.text = "勝ち";
        }
        else
        {
            m_txtResult.text = "負け";
        }
    }

    public int[] m_iScoreFirstAttack;
    public int[] m_iScoreSecondAttack;
    public TextMeshProUGUI[] m_txtScoreFirst;
    public TextMeshProUGUI[] m_txtScoreSecond;
    public TextMeshProUGUI m_txtScoreFirstTotal;
    public TextMeshProUGUI m_txtScoreSecondTotal;

    public void OnInningResult()
    {
        int iTotalFirst = 0;
        int iTotalSecond = 0;
        m_iScoreFirstAttack = new int[9];
        m_iScoreSecondAttack = new int[9];

        for (int i = 0; i < 9; i++)
        {
            m_iScoreFirstAttack[i] = 0;
            m_iScoreSecondAttack[i] = 0;

            int[] getScoreProb = new int[] { 70, 30 };
            while (0 < UtilRand.GetIndex(getScoreProb))
            {
                m_iScoreFirstAttack[i] += 1;
            }
            m_txtScoreFirst[i].text = m_iScoreFirstAttack[i].ToString();

            if (i == 8)
            {
                iTotalFirst = 0;
                iTotalSecond = 0;
                foreach (int score in m_iScoreFirstAttack)
                {
                    iTotalFirst += score;
                }
                foreach (int score in m_iScoreSecondAttack)
                {
                    iTotalSecond += score;
                }
                if (iTotalFirst < iTotalSecond)
                {
                    m_iScoreSecondAttack[i] = -1;
                    m_txtScoreSecond[i].text = "x";
                    break;
                }
            }
            while (0 < UtilRand.GetIndex(getScoreProb))
            {
                m_iScoreSecondAttack[i] += 1;
            }
            m_txtScoreSecond[i].text = m_iScoreSecondAttack[i].ToString();
        }
        iTotalFirst = 0;
        iTotalSecond = 0;
        foreach (int score in m_iScoreFirstAttack)
        {
            iTotalFirst += score;
        }
        foreach (int score in m_iScoreSecondAttack)
        {
            iTotalSecond += score;
        }
        m_txtScoreFirstTotal.text = iTotalFirst.ToString();
        m_txtScoreSecondTotal.text = iTotalSecond.ToString();


    }



}
