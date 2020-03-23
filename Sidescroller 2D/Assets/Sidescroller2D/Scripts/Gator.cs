using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : Enemy
{
    [SerializeField] float speed = 11f;
    public bool moveDown;
    private void Update()
    {
        if (moveDown)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            transform.position = temp;
        }
        else
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;
            transform.position = temp;
        }
    }
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.collider.name == "Turn" || target.collider.CompareTag("Turn"))
        {
            moveDown = !moveDown;
        }
    }
}
