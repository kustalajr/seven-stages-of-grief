using Cinemachine;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleporters : MonoBehaviour
{
    public PolygonCollider2D confiner;
    public CinemachineConfiner VirtualCamera;
    public Transform destination;
    public Transform GetDestination()
    {  
        return destination;

    }

    public void SwitchConfiner()
    {
        VirtualCamera.m_BoundingShape2D = confiner;
    }


}
