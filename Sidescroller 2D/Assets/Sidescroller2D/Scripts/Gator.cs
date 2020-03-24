using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gator : Enemy
{
    [SerializeField] float speed;

    private Vector3 positionA;
    private Vector3 positionB;
    private Vector3 nexPosition;

    private Transform objectTransform;
    [SerializeField] private Transform transformArrival;

    private void Awake()
    {
        objectTransform = this.transform;
    }
    private void Start()
    {
        positionA = objectTransform.localPosition;
        positionB = transformArrival.localPosition;
        nexPosition = positionB;
    }
    private void Update()
    {
        MovementGator();
    }
    private void MovementGator()
    {
        objectTransform.localPosition = Vector3.MoveTowards
            (objectTransform.localPosition, nexPosition, speed * Time.deltaTime);

        if (Vector3.Distance(objectTransform.localPosition, nexPosition) <= .1)
        {
            ChangeDestination();
        }
    }

    private void ChangeDestination()
    {
        nexPosition = nexPosition != positionA ? positionA : positionB;

    }
    //private void OnCollisionEnter2D(Collision2D target)
    //{
    //    if (target.collider.name == "Turn" || target.collider.CompareTag("Turn"))
    //    {
    //        moveDown = !moveDown;
    //    }
    //}
}
