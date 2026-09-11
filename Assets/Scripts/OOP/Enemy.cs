using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    public float ms = 2f;
    public float Distance = 0.5f;
    private float leftEdge;
    private float rightEdge;

    bool moveRight = true;

    // Damage yang diterima enemy setiap kali menabrak Player.
    [SerializeField] private int damageSaatTabrakan = 20;

    protected Transform player;

    [Header("State Machine")]
    [SerializeField] private float JarakDeteksi = 6f;
    [SerializeField] private float JarakSerang = 1.5f;
    [SerializeField] private float JedaSerang = 1f;

    // State Now
    private EnemyState state = EnemyState.IDLE;
    private float waktuSerangTerakhir;


    public static event Action<Enemy> OnZombieMati;
    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        rightEdge = transform.position.x + Distance;
        leftEdge = transform.position.x - Distance;
    }

    void Update()
    {
        PeriksaTransisi();
        
        switch(state)
        {
            case EnemyState.IDLE: PerilakuIdle(); break;
            case EnemyState.PATROL: PerilakuPatrol(); break;
            case EnemyState.CHASE: PerilakuChase(); break;
            case EnemyState.ATTACK: PerilakuAttack(); break;
        }
    }

    public float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity   ;
        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= JarakSerang)
        {
            state = EnemyState.ATTACK;
        }
        else if (jarak <= JarakDeteksi)
        {
            state = EnemyState.CHASE;
        }
        else
        {
            state = EnemyState.PATROL;
        }  
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }
    public void Patrol()
    {
        if (moveRight == true)
        {
            transform.Translate(Vector2.right * ms * Time.deltaTime);
            if(transform.position.x >= rightEdge)
            {
                Debug.Log("Nyampe Ujung kanan");
                moveRight = false;
            }
        }else if (moveRight == false)
        {
            transform.Translate(Vector2.left * ms * Time.deltaTime);
            if(transform.position.x <= leftEdge)
            {
                moveRight = true;
            }
        }
    }

    public virtual void Serang()
    {
        Debug.Log("Enemy menyerang!");
    }

    void PerilakuIdle()
    {
        Debug.Log("Enemy sedang IDLE");
    }

    void PerilakuPatrol()
    {
        Patrol();
        Debug.Log("Enemy sedang PATROL");
    }

    void PerilakuChase()
    {
        Kejar();
        Debug.Log("Enemy sedang CHASE");
    }

    void PerilakuAttack()
    {
        Debug.Log("Enemy sedang ATTACK");
    }

    public void KenaDamage(int damage)
    {
        hp -= damage;
        Debug.Log("Enemy kena damage: " + damage + ", HP sekarang: " + hp);
        
        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log("Enemy mati!");
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable playerScript = other.GetComponent<IDamageable>();
            if (playerScript != null)
            {
                playerScript.KenaDamage(damageSaatTabrakan);
            }
        }
    }
}
