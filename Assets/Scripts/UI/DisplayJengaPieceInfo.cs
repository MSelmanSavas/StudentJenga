using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayJengaPieceInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _infoText;
    [SerializeField] GameObject _infoPanel;

    public void UpdateJengaPieceInfoText(IJengaBrick jengaPiece)
    {
        if (jengaPiece == null)
        {
            _infoText.text = "Cannot display info...";
            return;
        }

        SubjectInfo subjectInfo = jengaPiece.GetSubjectInfo();

        _infoText.text = $"{subjectInfo.Grade}: {subjectInfo.Domain}\n{subjectInfo.Cluster}\n{subjectInfo.StandardID}: {subjectInfo.StandardDescription}";
    }

    public void EnableInfoText()
    {
        _infoText.enabled = true;
        _infoPanel.SetActive(true);
    }

    public void DisableInfoText()
    {
        _infoText.enabled = false;
        _infoPanel.SetActive(false);
    }
}
