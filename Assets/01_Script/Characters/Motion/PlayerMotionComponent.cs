using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Events;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMotionComponent : MonoBehaviour
{
    [Header("Speed/Timer")]
    public float speed = 10.0f;
    public float dashTimer = 10f;
    public float dashIntensity = 1000f;

    [Header("MouvementParam")]
    public Rigidbody2D rb;
    public Vector2 movement;
    public bool canMove = true;

    public event Action onDashStart;
    public event Action onDashStop;
    public event Action onWalk;
    public event Action onStop;


    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void moveCharacter(Vector2 direction)
    {
        if (canMove)
        {
            onWalk.Invoke();
            rb.velocity = direction * speed;
            Vector2 newDir = new Vector2(direction.x, direction.y);
            rb.velocity = newDir * speed;
        }
    }

    public void Dash(Vector2 direction)
    {
        if (canMove)
        {
            canMove = false;
            rb.AddForce(direction * dashIntensity);
            Vector2 newDir = new Vector2(direction.x, direction.y);
            rb.AddForce(newDir * dashIntensity);
            StartCoroutine(DashTimer());
        }
    }

    public void RotatePlayer(Transform transform)
    {
        Vector3 direction = transform.position - this.transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        this.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator DashTimer()
    {
        onDashStart?.Invoke();
        yield return new WaitForSeconds(dashTimer);
        onDashStop?.Invoke();
        canMove = true;
        yield return null;
    }

    internal void Stop()
    {
        onStop?.Invoke();
    }
}
