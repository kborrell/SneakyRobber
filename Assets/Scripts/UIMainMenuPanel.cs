using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuPanel : UIPanel {

	public void OnStartPressed()
    {
        InputManager.Instance.SetInputEnabled(true);
        UIManager.Instance.ShowPanel(UIManager.UIPanelKey.Gameplay);
    }
}
