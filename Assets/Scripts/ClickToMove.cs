using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour 
{
	//public float speed;
	public CharacterController controller;
    public Animator anim;

    CombatManager combatManager;
    Vector3 position;
    NavMeshAgent navMesh;
    float weaponRange;
    bool somethingCollided;


    // Use this for initialization
    void Start () 
	{
		position = transform.position;
        combatManager = GetComponent<CombatManager>();
        anim = GetComponent <Animator>();
        navMesh = GetComponent<NavMeshAgent>();
        weaponRange = GetComponentInChildren<WeaponScript>().weaponRange;
        somethingCollided = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit Hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out Hit, 1000)) //something at 1000 distance was hit!
            {
                if (Hit.transform.CompareTag("Enemy"))
                { //is it an big bad enemey? attack!
                  //combatManager.startAttack();
                    position = Hit.transform.position;
                    navMesh.stoppingDistance = weaponRange  + 2f;
                    MoveAttack(position);
                }
                else {
                    //combatManager.stopAttack();
                    position = Hit.point;
                    navMesh.stoppingDistance = 0;
                    Move(position);
                }
                
            }
        }
    }

    void FixedUpdate()
    {
        if(navMesh.remainingDistance <= 2 || somethingCollided)
        {
            anim.SetFloat("MovingSpeed", 0);
        }
        else
        {
            anim.SetFloat("MovingSpeed", 1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        somethingCollided = true;
    }
    void OnTriggerExit(Collider other)
    {
        somethingCollided = false;
    }

    void Move(Vector3 position)
    {
        /*Using Nav Mesh Agent*/
        navMesh.SetDestination(position);
        navMesh.Resume();
        /*rotate*/
        Quaternion newRotation = Quaternion.LookRotation(position - transform.position);
        newRotation.x = 0f;
        newRotation.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 10);
    }

    void MoveAttack(Vector3 position)
	{
        Move(position);
        
        if (navMesh.remainingDistance <= weaponRange || somethingCollided)
        {
            combatManager.startAttack();
        }
    }
}
