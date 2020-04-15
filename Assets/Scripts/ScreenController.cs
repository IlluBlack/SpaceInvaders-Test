using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public static float minPosX
    {
        get { return Instance.minPos.x; }
    }
    public static float maxPosX
    {
        get { return Instance.maxPos.x; }
    }

    public static float minPosY
    {
        get { return Instance.minPos.y; }
    }
    public static float maxPosY
    {
        get { return Instance.maxPos.y; }
    }

    private Vector2 minPos;
    private Vector2 maxPos;

    private static ScreenController Instance;

    private void Awake()
    {
        Instance = this;

        minPos = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxPos = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
}
