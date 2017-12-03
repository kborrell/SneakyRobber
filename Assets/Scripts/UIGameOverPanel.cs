using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPanel : UIPanel
{
    public void OnRestartPressed()
    {
        Application.LoadLevel("SceneVisual");
    }

    private void OnScoreChanged(int score)
    {
        m_scoreLabel.text = "SCORE: " + score;
    }

    private void Awake()
    {
        PlayerData.OnCoinsAmountChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        PlayerData.OnCoinsAmountChanged -= OnScoreChanged;
    }

    public Text m_scoreLabel;
}

