using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {

    public void SetItem(int index, TreasureData data)
    {
        m_itemIndex = index + 1;
        m_indexLabel.text = m_itemIndex.ToString();
        m_itemImage.sprite = data.GetItemSprite();
        m_targetKey = m_itemIndex.ToString();
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_targetKey))
        {
            if (OnInventoryItemSelected != null)
            {
                OnInventoryItemSelected(m_itemIndex - 1);
            }
        }
    }

    private string m_targetKey;

    public Text m_indexLabel;
    public Image m_itemImage;
    public int m_itemIndex;

    public delegate void OnInventoryItemSelectedDelegate(int index);
    public static OnInventoryItemSelectedDelegate OnInventoryItemSelected;
}
