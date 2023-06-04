using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;

public class LobbyEnchancedScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{

    private List<LobbyInfoEnhancedScrollerData> _data = new List<LobbyInfoEnhancedScrollerData>();
    public List<LobbyInfoEnhancedScrollerData> Data => _data;

    public EnhancedScroller myScroller;
    public LobbyEnhancedScollerCellView lobbyCellViewPrefab;

    void Start()
    {
        myScroller.Delegate = this;
        myScroller.ReloadData();
    }
    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100f;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int
    dataIndex, int cellIndex)
    {
        LobbyEnhancedScollerCellView cellView = scroller.GetCellView(lobbyCellViewPrefab) as
        LobbyEnhancedScollerCellView;
        cellView.SetData(_data[dataIndex]);
        return cellView;

    }
}

