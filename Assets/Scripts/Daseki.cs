﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DASEKI_RESULT
{
    STRIKE_OUT,   // 見逃し三振
    SWING_OUT,   // 空振り三振

    FOUR_BALL,   // フォアボール

    BONDA,

    SINGLE,
    TWOBASE,
    THREEBASE,
    HOMERUN,

    MAX
}

public enum PITCH_TYPE
{
    FASTBALL,
    CURVE,
    SLIDER,
    SHOOT,
    SINKER,
    SCREWBALL,
    FORK,
    SPLITTER,
    CHANGEUP,
    CUTTER,
    PALM,
    KNUCKLE,
    TWOSEAM,
    MAX
}

public struct PitchingBall
{
    public float speed;
    public Vector2 course;
    public PITCH_TYPE pitchType;
}

public class Daseki : MonoBehaviour
{
    public TextMeshProUGUI m_txtDasekiKekka;
    public DASEKI_RESULT GetDasekiResult()
    {
        int[] dasekiResultProb = new int[(int)DASEKI_RESULT.MAX]
        {
             50,        // 見逃し
             50,        // 空振り
             50,        // フォアボール
            500,        // 凡打（アウト）
            200,        // ヒット
             50,        // ツーベース
              5,        // スリーベース
             10,        // ホームラン
        };

        DASEKI_RESULT ret = (DASEKI_RESULT)UtilRand.GetIndex(dasekiResultProb);
        m_txtDasekiKekka.text = ret.ToString();

        return ret;
    }
    public bool IsOut(DASEKI_RESULT _result)
    {
        bool[] isOutResult = new bool[(int)DASEKI_RESULT.MAX]
        {
            true,        // 見逃し
            true,        // 空振り
            false,        // フォアボール
            true,        // 凡打（アウト）
            false,        // ヒット
            false,        // ツーベース
            false,        // スリーベース
            false,        // ホームラン
        };
        return isOutResult[(int)_result];
    }
    public int GetAdvanceCount(DASEKI_RESULT _result)
    {
        int[] dasekiAdvance = new int[(int)DASEKI_RESULT.MAX]
        {
             0,        // 見逃し
             0,        // 空振り
             1,        // フォアボール
             0,        // 凡打（アウト）
             1,        // ヒット
             2,        // ツーベース
             3,        // スリーベース
             4,        // ホームラン
        };
        return dasekiAdvance[(int)_result];
    }

    public void TestDasekiResult()
    {
        DASEKI_RESULT daseki = GetDasekiResult();
        m_txtDasekiKekka.text = daseki.ToString();
    }

    public PitchingBall Pitching()
    {
        PitchingBall ret = new PitchingBall();

        float fBias = 0.2f;
        Vector2 cource = new Vector2(
            Random.Range(0f - fBias, 3f + fBias),
            Random.Range(0f - fBias, 3f + fBias)
            );

        int iPitchType = Random.Range(0, (int)PITCH_TYPE.MAX);

        ret.course = cource;
        ret.pitchType = (PITCH_TYPE)iPitchType;

        return ret;
    }






}
