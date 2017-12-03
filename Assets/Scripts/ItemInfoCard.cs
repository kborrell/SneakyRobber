using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoCard : MonoBehaviour {

    public void SetItemData(TreasureData data)
    {
        m_itemValue.text = data.GetValue().ToString();
        m_itemWeight.text = data.GetWeight().ToString();
    }

    public Text m_itemValue;
    public Text m_itemWeight;
}
