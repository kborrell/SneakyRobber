using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour {

	public enum UIPanelKey
    {
        MainMenu,
        Gameplay,
        GameOver
    }

    [Serializable]
    public struct UIPanelData
    {
        public UIPanelKey key;
        public UIPanel panel;
    }

    public void ShowPanel(UIPanelKey key)
    {
        if (m_currentPanel != null)
        {
            m_currentPanel.Hide();
        }

        var panel = m_panels[key];
        panel.Show();
        m_currentPanel = panel;
    }

    public void HidePanel(UIPanelKey key)
    {
        if (m_currentPanel != null)
        {
            m_currentPanel.Hide();
            m_currentPanel = null;
        }
    }

    private void Awake()
    {
        foreach (UIPanelData data in m_panelList)
        {
            m_panels[data.key] = data.panel;
            data.panel.Hide();
        }

        ShowPanel(UIPanelKey.Gameplay);
    }

    public List<UIPanelData> m_panelList;

    private UIPanel m_currentPanel;
    private Dictionary<UIPanelKey, UIPanel> m_panels = new Dictionary<UIPanelKey, UIPanel>();
}
