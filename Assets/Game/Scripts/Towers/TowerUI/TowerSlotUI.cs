using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerSlotUI : TowerUI
{
    [SerializeField] private UpdateTowerInfor archer;
    private Transform uIImage;
    protected override void LoadComponent()
    {
        base.LoadComponent();
        archer ??= new UpdateTowerInfor
        {
            btn = optionsHolder.Find("Archer").GetComponentInChildren<Button>(),
            cost = optionsHolder.Find("Archer").GetComponentInChildren<TextMeshProUGUI>()
        };
    }

    private void Start()
    {
        archer.btn.onClick.AddListener(() =>
        {
           
            UpdateTower(archer.towerUpdate,archer.animUpdateName,1);
        });
    }
}
