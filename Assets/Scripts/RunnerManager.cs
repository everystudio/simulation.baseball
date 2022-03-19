using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunnerManager : MonoBehaviour
{
    public GameObject[] m_goRunners;
    public ShowBaseCondition m_showBaseCondition;
    public List<int> RunnerPosition = new List<int>();

    public void Clear()
    {
        RunnerPosition.Clear();
        ShowRunner();
    }

    public void ShowRunner()
    {
        for (int i = 0; i < m_goRunners.Length; i++)
        {
            m_goRunners[i].SetActive(RunnerPosition.Contains(i));
        }
        m_showBaseCondition.Show(RunnerPosition);
    }

    public void AddBatter(int _iAdd)
    {
        if (0 < _iAdd)
        {
            RunnerPosition.Add(0);
        }
        Advance(_iAdd);
        ShowRunner();
    }

    public void Advance(int _iAdd)
    {
        for (int i = 0; i < RunnerPosition.Count; i++)
        {
            RunnerPosition[i] += _iAdd;
        }
    }

    public int GetScore()
    {
        int iRetScore = 0;
        iRetScore = RunnerPosition.FindAll(pos => 4 <= pos).Count;
        RunnerPosition.RemoveAll(pos => 4 <= pos);
        return iRetScore;
    }

}
