using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float speed;
    public int damage;
    public float radius;
    public Collider2D[] hits;
    private Rigidbody2D rb;
    public GameObject target;
    public GameObject Explosion;
    public GameObject MissileObj;
    public BaseTurret sender;
    private Vector3 LastEnemyPos;
    private bool IsExploding;
    public bool moneyUpgrade, IsHoming;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LastEnemyPos = target.transform.position;
        transform.LookAt(target.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            if(!IsExploding)
            {
                
                Explode();
            }           
        }
    }


    public void Explode()
    {
        if(!IsExploding)
        hits = Physics2D.OverlapCircleAll(transform.position, radius);
        IsExploding = true;
        foreach (Collider2D enemy in hits)
        {
            if (enemy != null && enemy.tag == "Enemy")
            {               
                enemy.GetComponent<IDamagable>().TakeDamage(damage, moneyUpgrade);
                sender.KillCount++;
                Player.Instance.UpdateKillCount();
                StartCoroutine(Boom());
            }
        }
        StartCoroutine(Boom());
    }
    
    private IEnumerator Boom()
    {
        MissileObj.SetActive(false);
        Explosion.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if(!IsHoming)
        {
            if (!IsExploding)
            {
                rb.transform.position = Vector3.MoveTowards(transform.position, LastEnemyPos, speed * Time.deltaTime);
                if (transform.position == LastEnemyPos)
                    Explode();

            }
        }
        else
        {
            if (target != null)
            {
                LastEnemyPos = target.transform.position;
                rb.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                transform.LookAt(target.transform);
            }
            else if (target == null)
            {
                //if current target gets destroyed, the rocket will fly to the destroyed enemies last position then blow up.
                transform.LookAt(LastEnemyPos);
                rb.transform.position = Vector3.MoveTowards(transform.position, LastEnemyPos, speed * Time.deltaTime);
                if (transform.position == LastEnemyPos)
                    Explode();
            }
        }       
    }
}
