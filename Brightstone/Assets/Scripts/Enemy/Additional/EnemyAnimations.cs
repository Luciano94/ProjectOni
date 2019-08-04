﻿using UnityEngine;

public class EnemyAnimations : MonoBehaviour{
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyCombat enemyCombat;
    [SerializeField] private Color hitColor;

    private SpriteRenderer sprRenderer;
    private float speed = 1.0f;
    private bool isHit = false;
    private bool isLowHealth = false;

    private void Start(){
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        if (isLowHealth && !isHit){
            float perc = Mathf.PingPong(Time.time * 0.5f, 1.0f - hitColor.g);
            sprRenderer.color = new Color(hitColor.r, hitColor.g - perc, hitColor.b - perc);
        }
    }

    public void SetDirection(int dir){
        anim.SetInteger("Direction", dir);
    }

    public void SetAttack(){
        anim.SetTrigger("Attack");
        //anim.SetFloat("AnimSpeed", enemyCombat.AnimTime);
    }

    public void Idle(){
        anim.ResetTrigger("Move");
        anim.SetTrigger("Idle");
    }

    public void Move(){
        anim.ResetTrigger("Idle");
        anim.SetTrigger("Move");
    }

    public void MovingBackwards(){
        anim.SetFloat("AnimSpeed", speed * 1.0f);
    }

    public void StopMovingBackwards(){
        anim.SetFloat("AnimSpeed", speed * -1.0f);
    }

    public void UpdateSpeed(float speed){
        float sign = Mathf.Sign(anim.GetFloat("AnimSpeed"));
        this.speed = speed * sign;
        anim.SetFloat("AnimSpeed", speed);
    }

    public void Hit(){
        isHit = true;
        sprRenderer.color = hitColor;
        anim.SetTrigger("Hit");
    }

    public void Restore(){
        isHit = false;
        sprRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        anim.SetTrigger("Restore");
    }

    public void IsLowHealth(){
        isLowHealth = true;
    }

    public void Death(){
        float dir = anim.GetInteger("Direction");
        anim.SetFloat("Dir", dir);

        float whichDeath = Random.Range(0, 3);
        anim.SetFloat("WhichDeath", whichDeath);

        anim.SetTrigger("Death");

        transform.SetParent(null);

        Autodestroy autoD = gameObject.AddComponent<Autodestroy>() as Autodestroy;
        autoD.PrepareValues(5.5f);

        VanishToDeath vanishD = gameObject.AddComponent<VanishToDeath>() as VanishToDeath;
        vanishD.PrepareValues(3.0f, 2.0f);
    }
}
