using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{

    public int hp;
    public Animator anim; //need to use combat animations
    WeaponScript weaponScript;
    bool attackReady;
    float attackCD;

    // Use this for initialization
    void Start()
    {
        if (hp == 0)
        {
            hp = 100;
        }
        if(anim == null)
            anim = GetComponent<Animator>();
        weaponScript = this.GetComponentInChildren<WeaponScript>();
        attackCD = weaponScript.weaponSpeed;
        attackReady = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        anim.SetBool("Attack", false);
    }

    public void startAttack() {
        if (attackReady) { 
            anim.SetBool("Attack", true);
            attackReady = false;
            Invoke("resetAttack", attackCD);

            if (weaponScript.target != null) //enemy is in weapon range
            {
                weaponScript.target.GetComponent<CombatManager>().hp -= weaponScript.weaponDmg; //deal DAMAGES!
                if (weaponScript.target.GetComponent<CombatManager>().hp <= 0) // if target is dead
                {
                    Destroy(weaponScript.target.gameObject/*, 0.5f*/);
                    this.stopAttack(); //target died
                }
            }
        }
    }

    public void resetAttack()
    {
        attackReady = true;
        anim.SetBool("Attack", false);
    }

    public void stopAttack()
    {
        anim.SetBool("Attack", false);
    }
}