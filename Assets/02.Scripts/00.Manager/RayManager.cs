using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 필요한가?
/// </summary>
public class RayManager : MonoBehaviour
{
    public static RayManager Instance;

    Camera cam;
    int layerMask;
    public RaycastHit2D hit;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        layerMask = ~(LayerMask.GetMask("UI"));
    }

    public void Update()
    {
        hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, layerMask);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider == null) return;
            else print("Click Detected : " + hit.collider.gameObject);
        }

    }
}
