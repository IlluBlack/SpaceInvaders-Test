using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private float maxPosX;
    private float minPosX;
    private float radio;

    private void Start()
    {
        radio = this.gameObject.GetComponent<CapsuleCollider2D>().size.x / 2;
        SetBounds();
    }

    private void SetBounds()
    {
        maxPosX = ScreenController.maxPosX + radio;
        minPosX = ScreenController.minPosX - radio;
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        if (xAxis != 0)
            Move(xAxis);
    }

    private void Move(float value)
    {
        Vector2 pos = this.transform.position;
        pos.x += value * speed * Time.deltaTime;

        //Bounds
        if(pos.x >= maxPosX)
            pos.x = minPosX;
        else if(pos.x <= minPosX)
            pos.x = maxPosX;

        this.transform.position = pos;
    }

}
