using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    public float speed;
    public float range;
    public Transform player;
    public CharacterController controller;
    public Animator anim;

    CombatManager combatManager;
    NavMeshAgent navMesh;

    // Use this for initialization
    void Start()
    {
        combatManager = GetComponent<CombatManager>();
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        { //if Player is present on scene

            transform.LookAt(player.position);

            if (!inRange())
            {
                Chase();
                anim.SetFloat("MovingSpeed", 1);
                combatManager.stopAttack();
            }
            else
            {
                anim.SetFloat("MovingSpeed", 0);
                combatManager.startAttack();
            }
        }
    }

    bool inRange()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < range)
            return true;
        return false;
    }

    void Chase()
    {
        //controller.SimpleMove(transform.forward * speed);
        navMesh.SetDestination(player.position);
    }

}
