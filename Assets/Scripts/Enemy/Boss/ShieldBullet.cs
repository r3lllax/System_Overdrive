using UnityEngine;

public class ShieldBullet : MonoBehaviour
{
    private GameObject owner;
    private SpriteRenderer sp;
    private Animator anim;

    private float PlayerSizeMultiplier;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        PlayerSizeMultiplier = transform.localScale.x / 2;
    }

    public void SetOnwer(GameObject newOwner)
    {
        owner = newOwner;
    }

    private void Update()
    {

        if (owner)
        {
            if (owner.gameObject.tag == "Player")
            {
                anim.enabled = true;
                transform.localScale = new Vector3(PlayerSizeMultiplier, PlayerSizeMultiplier, transform.localScale.z);
            }
            else
            {
                anim.enabled = false;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && owner.tag != "Player")
        {
            collision.GetComponent<Player>().TakeDamage(1);
        }
        if ((collision.tag == "Enemy" || collision.tag == "Boss") && owner.tag == "Player")
        {
            collision.GetComponent<Enemy>().TakeDamage((int)(SessionData.Damage * SessionData.CritScale), 5f, "crit");
        }
    }
    

}
