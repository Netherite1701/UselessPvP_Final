using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;
using TMPro;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    #region Variables
    float playerHeight = 2f;

    [SerializeField] Transform orientation;
    [SerializeField] AimingScript aimingScript;
    [SerializeField] GameObject eyes;
    [SerializeField] WeaponRecoil weaponRecoil;
    [SerializeField] GunScript gunScript;
    [SerializeField] PlayerFire playerFire;
    [SerializeField] WallRun wallRunScript;
    [SerializeField] PlayerWeapon playerWeapon;

    [Header("Movement")]
    float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;

    [Header("Double Jump")]
    [SerializeField] bool hasDoubleJumped = false;
    public bool hasJumped = false;
    bool wallrunJumped = false;
    [SerializeField] GameObject redImg;
    [SerializeField] GameObject greenImg;

    [Header("Dash")]
    [SerializeField] float dashCooldown = 4f;
    [SerializeField] float dashForce;
    [SerializeField] float dashUpForce;
    [SerializeField] bool cooltime = false;
    int dashCount = 0;
    float movementMultiplier = 10f;
    KeyCode dashKey = KeyCode.V;
    [SerializeField] public TMP_Text dashUI;

    [Header("Sprinting")]
    float walkSpeed = 6f;
    float sprintSpeed = 10f;
    [SerializeField] float acceleration = 50f;
    [SerializeField] float crouchSpeed = 3f;

    [Header("Jumping")]
    public float jumpForce = 5f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.3f;
    public bool isGrounded { get; private set; }

    [Header("Crouching")]
    [SerializeField] CapsuleCollider capsuleCollider;
    public bool isCrouching = false;
    [SerializeField] GameObject viewModel;

    [Header("Misc")]
    [SerializeField] float aimWalk = 2;
    [SerializeField] float aimSprint = 4;
    [SerializeField] float normalWalk = 6;
    [SerializeField] float normalSprint = 10;



    public int itemindex = 0;
    public int previousItemIndex = -1;

    [Header("Item System")]
    public Item[] items;
    Item currentItem;

    public GameObject[] itemGO = new GameObject[6];

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    PhotonView PV;

    RaycastHit slopeHit;


    PlayerManager playerManager;

    PlayerClass playerClass;

    Variables var;

    string className;

    public bool canMove = true;

    [SerializeField]
    float jumpPadBoost = 100000f;
    #endregion


    private void Awake()
    {
        var = GetComponent<Variables>();
        playerClass = GetComponent<PlayerClass>();
        className = playerClass.ReturnClassName();
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }
    #region Movement

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        if (PV.IsMine)
        {
            eyes.SetActive(false);
            EquipItem(0);
        }
        else
        {
            Destroy(rb);
        }
        for(int i = 0; i < items.Length; i++)
        {
            itemGO[i] = items[i].itemGameObject.transform.parent.gameObject;
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        if (canMove == true)
        {
            transform.GetComponent<Rigidbody>().isKinematic = false;
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            MyInput();
            ControlDrag();
            ControlSpeed();
            if (isGrounded && hasDoubleJumped == true)
            {
                hasDoubleJumped = false;
                hasJumped = false;
            }
            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                Jump();
            }
            if (Input.GetKeyDown(jumpKey) && hasDoubleJumped == false && isGrounded == false && wallRunScript.wallLeft == false && wallRunScript.wallRight == false && hasJumped == true)
            {
                hasDoubleJumped = true;
                Jump();
            }
            redImg.SetActive(hasDoubleJumped);
            greenImg.SetActive(!hasDoubleJumped);

            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

            if (transform.position.y < -10f)
            {
                Die();
            }

            if (aimingScript.aiming)
            {
                walkSpeed = aimWalk;
                sprintSpeed = aimSprint;
            }

            else
            {
                walkSpeed = normalWalk;
                sprintSpeed = normalSprint;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Crouch();
            }
            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                CancelCrouch();
            }

            if (Input.GetKeyDown(dashKey) && cooltime == false)
            {
                StartCoroutine(Dash());
            }
        }
        else
        {
            transform.GetComponent<Rigidbody>().isKinematic = true;
        }
        //if(gunScript.isReloading == false)
        //{
        if (className == "Sniper")
        {
            if (itemindex != 2)
            {
                for (int i = 0; i < items.Length - 1; i++)
                {
                    if (Input.GetKeyDown((i + 1).ToString()))
                    {
                        EquipItem(i);
                        break;
                    }
                }
                if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
                {
                    if (itemindex >= items.Length - 1 - 1)
                    {
                        EquipItem(0);
                    }
                    else
                    {
                        EquipItem(itemindex + 1);
                    }
                }
                if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
                {
                    if (itemindex <= 0)
                    {
                        EquipItem(items.Length - 1 - 1);
                    }
                    else
                    {
                        EquipItem(itemindex - 1);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    EquipItem(i);
                    break;
                }
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                if (itemindex >= items.Length - 1)
                {
                    EquipItem(0);
                }
                else
                {
                    EquipItem(itemindex + 1);
                }
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                if (itemindex <= 0)
                {
                    EquipItem(items.Length - 1);
                }
                else
                {
                    EquipItem(itemindex - 1);
                }
            }
        }
//        }
        currentItem = items[itemindex];
        
        //if(((GunInfo)gunScript.itemInfo).itemName != _currentWeapon)
        //{
        //    gunScript.StopFiring();
        //}
        if (currentItem.isGun)
        {
            gunScript = currentItem.gameObject.GetComponent<GunScript>();
            playerFire.gunScript = gunScript;
            weaponRecoil.itemInfo = gunScript.itemInfo;
            aimingScript.itemInfo = gunScript.itemInfo;
            aimingScript.gunScript = gunScript;
            weaponRecoil.UpdateRecoil();
            gunScript.UpdateUI();
        }
        else if (currentItem.isAbility){
            var abilityScript = currentItem.gameObject.GetComponent<AbilityScript>();
            playerFire.gunScript = null;
            weaponRecoil.itemInfo = null;
            aimingScript.itemInfo = null;
            aimingScript.gunScript = null;
            abilityScript.UpdateUI();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (currentItem.isGun)
                playerFire.StartFiring();
            else
                currentItem.Use();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            playerFire.StopFiring();
        }


    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        //if (isGrounded)
        //{
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        hasJumped = true;
        if(wallRunScript.wallLeft == true || wallRunScript.wallRight == true)
        {
            hasJumped = false;
        }
        //}
    }

    void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded) // Sprinting
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else if (isCrouching == true) // Crouching
        {
            moveSpeed = Mathf.Lerp(moveSpeed, crouchSpeed, acceleration * Time.deltaTime);
        }
        else // Walking
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (!PV.IsMine)
            return;

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }

    }

    void Crouch()
    {
        capsuleCollider.height = 1.5f;
        capsuleCollider.center = new Vector3(0, 0.25f, 0);
        viewModel.transform.localScale = new Vector3(0, 0.75f, 0);
        isCrouching = true;
    }

    void CancelCrouch()
    {
        capsuleCollider.height = 2;
        capsuleCollider.center = new Vector3(0, 0, 0);
        viewModel.transform.localScale = new Vector3(0, 1, 0);
        isCrouching = false;
    }

    IEnumerator Dash()
    {
        dashCount++;
        Vector3 dashVector = orientation.forward * dashForce + orientation.up * dashUpForce;
        rb.AddForce(dashVector, ForceMode.Impulse);
        if (dashCount > 1)
        {
            cooltime = true;
            dashCount = 0;
            dashUI.text = "Cooling Down";
            yield return new WaitForSeconds(dashCooldown);
            dashUI.text = "Dashes Left: " + (2 - dashCount);
            cooltime = false;
        }
        else
        {
            dashUI.text = "Dashes Left: " + (2 -dashCount);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject pad = collision.gameObject;
        if(pad.gameObject.tag == "JumpPad")
        {
            //Debug.Log("Pad Touched");
            rb.AddForce(0, jumpPadBoost, 0);
        }
    }
    #endregion



    #region Weapon System

    public GameObject SR;
    public void EquipItem(int _index)
    {
        if (gunScript != null)
        {
            if (gunScript.isReloading == true)
            {
                return;
            }
        }
        if (_index == previousItemIndex || aimingScript.aiming == true || var.abilityScript.usingAbility)
        {
            return;
        }
        itemindex = _index;
        SR = items[itemindex].itemGameObject;
        items[itemindex].itemGameObject.SetActive(true);
        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }
        previousItemIndex = itemindex;
        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemindex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }

    }
    void Die()
    {
        playerManager.Die();
    }


    #endregion
}