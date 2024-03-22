using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float _maxHealth;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _def;
    [SerializeField] private int _coins;

    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform spellPos;

    public InventoryObject Inventory;
    public CharacterController characterController;
    public Animator animator;
    public Transform cam;

    public float speed = 1f;
    public float gravity = -20f;
    public float jumpSpeed = 15;

    public float TurnSmothTime = 0.1f;
    private float turnSmothVelocity;
    private bool isDead;
    private bool isInventoryOpen;
    [SerializeField] private GameObject ipanel;

    Vector3 moveVelocity;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI coin;

    private void Start()
    {
        isInventoryOpen = false;
        DontDestroyOnLoad(this.gameObject);
        _maxHealth = _health;
        var normalizeHealth = _health / _maxHealth;
        slider.value = normalizeHealth;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isInventoryOpen = !isInventoryOpen;
            ipanel.SetActive(isInventoryOpen);
        }

        if (!isDead)
        {
            if (!isInventoryOpen) {
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");

                Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


                if (characterController.isGrounded)
                {
                    animator.SetBool("IsGrounded", false);
                    moveVelocity = transform.forward * speed * Time.deltaTime;
                    if (Input.GetButtonDown("Jump"))
                    {
                        animator.SetBool("IsGrounded", true);
                        moveVelocity.y = jumpSpeed;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(CreateSpellCoroutine());
                    }
                }
                moveVelocity.y += gravity * Time.deltaTime;
                characterController.Move(moveVelocity * Time.deltaTime);
                if (direction.magnitude >= 0.1f && !Input.GetKey(KeyCode.LeftShift))
                {
                    Walk(direction);
                }
                else if (direction.magnitude >= 0.1f && Input.GetKey(KeyCode.LeftShift))
                {
                    Run(direction);
                }
                else if (direction.magnitude == 0)
                {
                    Idle();
                } 
            }
        }
    }

    private void Idle() 
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
    }
    private void Walk(Vector3 direction)
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);
        var moveDir = CalculateNumbers(direction);
        characterController.Move(moveDir.normalized * speed * Time.deltaTime);
    }
    private void Run(Vector3 direction)
    {
        animator.SetBool("Run", true);
        animator.SetBool("Walk", false);
        var moveDir = CalculateNumbers(direction);
        characterController.Move(moveDir.normalized * (speed + 2f) * Time.deltaTime);
    }
    private Vector3 CalculateNumbers(Vector3 direction)
    {
        float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmothVelocity, TurnSmothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
        return moveDir;
    }

    public void getDamage(float Damage) 
    {
        _health -= Damage;
        var normalizeHealth = _health / _maxHealth;
        slider.value = normalizeHealth;
        if(_health <= 0) 
        {
            Die();
        }
    }
    public void getCoins(int Coins)
    {
        _coins += Coins;
        coin.text = _coins.ToString();
    }
    public void getItem(Item item)
    {
        Inventory.AddItem(item, 1);
    }
    private void CreateSpell() 
    {
        GameObject newSpell = Instantiate(spellPrefab, spellPos);

        newSpell.transform.SetParent(null);
    }
    private IEnumerator CreateSpellCoroutine()
    {
        CreateSpell();
        yield return new WaitForSeconds(2f);
    }
    private void Die() 
    {
        Destroy(this.gameObject);
    }
    //public void Save()
    //{
    //    Inventory.Save();
    //}
    //public void Load()
    //{
    //    Inventory.Load();
    //}
    private void OnApplicationQuit()
    {
        Inventory.Container.Items = new InventorySlot[40];
    }

}