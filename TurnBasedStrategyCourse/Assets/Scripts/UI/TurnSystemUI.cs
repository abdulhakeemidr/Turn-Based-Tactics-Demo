using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This updates the Turn System UI by updating the turn number text
// and giving a function call to the end turn button press
public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button EndTurnButton;
    [SerializeField] private TextMeshProUGUI TurnNumberText;

    private void Start()
    {
        EndTurnButton.onClick.AddListener(() => {
            TurnSystem.Instance.NextTurn();
        });
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UpdateTurnText();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
    }

    private void UpdateTurnText()
    {
        TurnNumberText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }
}
