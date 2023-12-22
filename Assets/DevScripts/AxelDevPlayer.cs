using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxelDevPlayer : MonoBehaviour
{
    public float speed = 1f;
    private Vector2 movement;

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(
            speed * inputX,
            speed * inputY);

    }

    void FixedUpdate()
    {
        transform.Translate(movement);
    }
}
