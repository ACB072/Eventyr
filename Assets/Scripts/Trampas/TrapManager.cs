using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public int damage;
    private bool flip, stop = false;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public Sprite spark;
    private Rigidbody2D body;
    private YsbridManager Ysbrid;
    private DewinManager Dewin;
    private CladdwydManager Cladd;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Ysbrid = YsbridManager.ysbridInstance;

        if (DewinManager.dewinInstance != null)
            Dewin = DewinManager.dewinInstance;

        if (CladdwydManager.claddInstance != null)
            Cladd = CladdwydManager.claddInstance;

        if (transform.position.x < 0)
        {
            spriteRenderer.flipX = true;
            flip = true;
        }
    }

    void Update()
    {
        if(!stop)
        {
            if (flip)
                body.MovePosition(new Vector2(transform.position.x + 10 * 1 * Time.deltaTime, transform.position.y));
            else
                body.MovePosition(new Vector2(transform.position.x - 10 * 1 * Time.deltaTime, transform.position.y));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ysbrid")
            StartCoroutine(Exploded());
    }

    IEnumerator Exploded()
    {
        stop = true;
        spriteRenderer.sprite = spark;
        anim.SetBool("Exploded", true);
        Ysbrid.health -= damage;
        Dewin.dewinHealth -= damage;
        Cladd.claddwydHealth -= damage;
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
