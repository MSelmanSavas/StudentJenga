using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DisplayStackInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _infoText;
    [SerializeField] GameObject _infoPanel;

    public void UpdateStackInfoText(IJengaStacker jengaStacker)
    {
        if (jengaStacker == null)
        {
            _infoText.text = "Cannot display info...";
            return;
        }

        List<SubjectInfo> subjectInfos = jengaStacker.GetSubjectInfos();

        _infoText.text = $"Selected Grade : {subjectInfos[0].Grade}";
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
