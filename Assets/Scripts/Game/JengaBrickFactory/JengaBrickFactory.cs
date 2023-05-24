using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaBrickFactory : IJengaBrickFactory
{
    ScriptableJengaPieceInfo _jengaPieceInfos;

    public JengaBrickFactory(ScriptableJengaPieceInfo jengaPieceInfos)
    {
        _jengaPieceInfos = jengaPieceInfos;
    }

    public bool TrySetJengaInfos(ScriptableJengaPieceInfo jengaPieceInfos)
    {
        _jengaPieceInfos = jengaPieceInfos;
        return true;
    }

    public bool TryGetJengaBrick(JengaFactoryData data, out IJengaBrick jengaBrick)
    {
        if (_jengaPieceInfos == null)
        {
            Debug.LogError("Error while creating jenga brick! Jenga Piece Infos is not set...");
            jengaBrick = null;
            return false;
        }

        if (_jengaPieceInfos.JengaInfos.Count <= 0)
        {
            Debug.LogError("Error while creating jenga brick! There is no jenga piece info data to use...");
            jengaBrick = null;
            return false;
        }

        if (!_jengaPieceInfos.JengaInfos.ContainsKey(data.MasteryLevel))
        {
            Debug.LogError($"Error while creating jenga brick! There is no jenga brick type found for : {data.MasteryLevel}...");
            jengaBrick = null;
            return false;
        }

        jengaBrick = GameObject.Instantiate(_jengaPieceInfos.JengaInfos[data.MasteryLevel].Prefab).GetComponent<IJengaBrick>();
        jengaBrick.ApplyMaterial(_jengaPieceInfos.JengaInfos[data.MasteryLevel].Material);
        jengaBrick.ApplyPhysicsMaterial(_jengaPieceInfos.JengaInfos[data.MasteryLevel].PhysicMaterial);
        return true;
    }

    public bool TryGetJengaBrickInfo(JengaFactoryData data, out JengaBrickInfo jengaBrick)
    {
        if (_jengaPieceInfos == null)
        {
            Debug.LogError("Error while creating jenga brick! Jenga Piece Infos is not set...");
            jengaBrick = null;
            return false;
        }

        if (_jengaPieceInfos.JengaInfos.Count <= 0)
        {
            Debug.LogError("Error while creating jenga brick! There is no jenga piece info data to use...");
            jengaBrick = null;
            return false;
        }

        if (!_jengaPieceInfos.JengaInfos.ContainsKey(data.MasteryLevel))
        {
            Debug.LogError($"Error while creating jenga brick! There is no jenga brick type found for : {data.MasteryLevel}...");
            jengaBrick = null;
            return false;
        }

        jengaBrick = _jengaPieceInfos.JengaInfos[data.MasteryLevel];
        return true;
    }
}
