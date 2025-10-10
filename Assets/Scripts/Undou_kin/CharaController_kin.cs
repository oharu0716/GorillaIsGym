using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharaController_kin : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator animator;
    float angle;
    bool isDead;

    public float maxHeight;
    public float flapVelocity;
    public float relativeVelocityX;
    public GameObject sprite;

    public bool IsDead()
    {
        return isDead;
    }

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && transform.position.y < maxHeight)
        {
            Flap();
        }
        ApplyAngle();
        animator.SetBool("flap", angle >= 0.0f && !isDead);

    }
    public void Flap()
    {
        if (isDead) return;
        // isKinematicをbodyTypeを使った判定に変更
        if (rb2d.bodyType == RigidbodyType2D.Kinematic) return;
        rb2d.linearVelocity = new Vector2(0.0f, flapVelocity);

    }
    void ApplyAngle()
    {
        float targetAngle;

        if (isDead)
        {
            targetAngle = 180f;

        }
        else
        {
            targetAngle =
            Mathf.Atan2(rb2d.linearVelocityY, relativeVelocityX) * Mathf.Rad2Deg;
        }
        angle = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10f);

        sprite.transform.localRotation = Quaternion.Euler(0, 0, angle);

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        Camera.main.SendMessage("Clash");
        isDead = true;

    }
    public void SetSteerActive(bool active)
    {
        // isKinematicをbodyTypeを使った設定に変更

        rb2d.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
    }
}