using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum 攻撃順
{
    先攻,
    後攻,
    最大
}

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
    public TextMeshProUGUI m_txtSayonara;

    public int[,] m_iScoreInning;

    public Image[] m_imgOutCount;

    public void ShowOutCount(int _iOutCount)
    {
        for (int i = 0; i < m_imgOutCount.Length; i++)
        {
            Color color = Color.white;
            if (i < _iOutCount)
            {
                color = Color.red;
            }
            m_imgOutCount[i].color = color;
        }
    }
    public void ShowScoreBoard(int[,] _scoreArr)
    {
        int senkoTotal = 0;
        int koukouTotal = 0;
        for (int i = 0; i < 9; i++)
        {
            senkoTotal += 0 < _scoreArr[0, i] ? _scoreArr[0, i] : 0;
            koukouTotal += 0 < _scoreArr[1, i] ? _scoreArr[1, i] : 0;
            m_txtScoreFirst[i].text = 0 <= _scoreArr[0, i] ? _scoreArr[0, i].ToString() : "";

            if (i < 8)
            {
                m_txtScoreSecond[i].text = 0 <= _scoreArr[1, i] ? _scoreArr[1, i].ToString() : "";
            }
            else
            {
                if (-1 == _scoreArr[1, i])
                {
                    m_txtScoreSecond[i].text = "";
                    m_txtSayonara.gameObject.SetActive(false);
                }
                else if (-2 == _scoreArr[1, i])
                {
                    m_txtScoreSecond[i].text = "x";
                    m_txtSayonara.gameObject.SetActive(false);
                }
                else
                {
                    m_txtScoreSecond[i].text = 0 <= _scoreArr[1, i] ? _scoreArr[1, i].ToString() : "";
                    m_txtSayonara.gameObject.SetActive(senkoTotal < koukouTotal);
                }
            }
        }
        m_txtScoreFirstTotal.text = senkoTotal.ToString();
        m_txtScoreSecondTotal.text = koukouTotal.ToString();
    }

    public bool CheckSayonara(int _iInning, 攻撃順 _junban, int[,] _score)
    {
        if (_iInning < 8)
        {
            return false;
        }
        else if (_junban == 攻撃順.先攻)
        {
            return false;
        }
        else
        {
        }

        int senkoTotal = 0;
        int koukouTotal = 0;
        for (int i = 0; i < 9; i++)
        {
            senkoTotal += 0 < _score[0, i] ? _score[0, i] : 0;
            koukouTotal += 0 < _score[1, i] ? _score[1, i] : 0;
        }
        return (senkoTotal < koukouTotal);
    }

    public IEnumerator InningResult(int _iInning, 攻撃順 _junban)
    {
        int iOutCount = 0;
        ShowOutCount(iOutCount);

        if (CheckSayonara(_iInning, _junban, m_iScoreInning))
        {
            m_iScoreInning[(int)_junban, _iInning] = -2;
            ShowScoreBoard(m_iScoreInning);
            yield return new WaitForSeconds(0.05f);
            yield break;
        }

        m_iScoreInning[(int)_junban, _iInning] = 0;

        for (iOutCount = 0; iOutCount < 3;)
        {
            ShowScoreBoard(m_iScoreInning);
            int[] resultProb = new int[] {
                90,     // アウト
                10      // 1点取れた
            };
            int iResult = UtilRand.GetIndex(resultProb);

            switch (iResult)
            {
                case 0:
                    iOutCount += 1;
                    ShowOutCount(iOutCount);

                    break;
                case 1:
                default:
                    m_iScoreInning[(int)_junban, _iInning] += 1;
                    break;
            }
            ShowScoreBoard(m_iScoreInning);
            yield return new WaitForSeconds(0.05f);

            if (CheckSayonara(_iInning, _junban, m_iScoreInning))
            {
                break;
            }
        }

    }
    public void OnInningResult2()
    {
        int[,] test = new int[3, 4];
        m_iScoreInning = new int[(int)攻撃順.最大, 9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < (int)攻撃順.最大; j++)
            {
                m_iScoreInning[j, i] = -1;
            }
        }

        StartCoroutine(InningOutCountBegin());
    }

    private IEnumerator InningOutCountBegin()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < (int)攻撃順.最大; j++)
            {
                yield return StartCoroutine(InningResult(i, (攻撃順)j));
            }
        }
    }

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
