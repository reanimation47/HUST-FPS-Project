using Player.Components;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace Player
{
    public class PlayerController : MonoBehaviourPunCallbacks, ICombat  
    {
        #region Initialize Variables
        public static CharacterController controller;
        public static Transform playerTransform;
        private Vector3 playerVelocity;
        private bool isGrounded;

        public static float playerSpeed { get; private set; }
        public static float interactDistance { get; private set; }
        public static LayerMask interactMask { get; private set; }
        public static TextMeshProUGUI promptMessage { get; set; }
        public static TextMeshProUGUI objectiveMessage { get; set; }
        public static GameObject characterHead;
        public static GameObject bodySpine;
        public static Animator characterAnimator;

        public static readonly float xSensitivity = 30f;
        public static readonly float ySensitivity = 30f;

        [Header("Player Basic Settings")]
        public float _playerSpeed = 5f;
        public float _crouchSpeed = 2f;
        public float gravity = -9.8f;
        public float jumpHeight = 1f;

        [Header("Player Interaction Settings")]
        public float _interactDistance = 3f;
        public LayerMask _interactMask;
        public TextMeshProUGUI _promptMessage;
        public TextMeshProUGUI _objectiveMessage;

        [Header("Character body controls")]
        public GameObject _characterHead;
        public GameObject _bodySpine;
        public Animator _characterAnimator;

        [Header("Others")]
        public PlayerHealth PlayerHealth;
        public PlayerWeapons PlayerWeapons;




        public Camera _cam;
        #endregion

        void Awake()
        {
            //Screen.SetResolution(1920, 1080, false);
            ICommon.LoadPlayer(this.gameObject);
            // if (!photonView.IsMine)
            // {
            //     Debug.LogError("Destroying player scripts");
            //     Destroy(this.gameObject);
            // }
        }

        void Start()
        {
            if (photonView.IsMine)
            {
                //Spawn();
                Cursor.lockState = CursorLockMode.Locked;
                AssignStaticVariables();
                AssignComponents();
                //PlayerUI.UpdateObjective();
                //ICommon.RemoveObjectFromAnimator(_cam.transform.gameObject, characterAnimator);
                //_cam.transform.gameObject.SetActive(true);
            }

        }

        private void Update()
        {
            if(photonView.IsMine)
            {
                isGrounded = controller.isGrounded;
                PlayerInteract.CheckInteraction();
                PlayerCrouch.ProcessCrouch();
                TestDmg();
            }
            
        }

        #region Assign static variables
        private void AssignStaticVariables()
        {
            controller = GetComponent<CharacterController>();
            playerTransform = transform;
            playerSpeed = _playerSpeed;
            interactDistance = _interactDistance;
            interactMask = _interactMask;
            promptMessage = _promptMessage;
            objectiveMessage = _objectiveMessage;
            characterHead = _characterHead;
            bodySpine = _bodySpine;
            characterAnimator = _characterAnimator;
        }

        #endregion

        #region Assign Components
        private void AssignComponents()
        {
            PlayerLookAround.AssignCamera(_cam);
            PlayerInteract.AssignCamera(_cam);
        }
        #endregion

        #region All Controls Processes
        public void ProcessMove(Vector2 input)
        {
            PlayerMovement.ProcessMove(input);
            ProcessGravity();
        }

        public void ProcessLookAround(Vector2 input)
        {
            PlayerLookAround.ProcessLookAround(input);
        }
        #endregion

        #region Binded actions

        public void Crouch()
        {
            playerSpeed = PlayerCrouch.Crouch() ? _crouchSpeed : _playerSpeed;
        }
        
        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3f * gravity);
            }
        }
        #endregion

        #region Gravity
        private void ProcessGravity()
        {
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = -2f;
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }
        #endregion

        #region Combat
        public void TakeDamage(float dmg)
        {
            Debug.LogWarning("con ");
            if(PlayerHealth.TakeDamage(dmg) <= 0)
            {
                Debug.LogError(dmg);
                //PlayerSpawner.Instance.Die();
            }
        }

        public void RestoreHealth(float hp)
        {
            PlayerHealth.RestoreHealth(hp);
        }
        #endregion

        #region GamePlay
        private void Spawn()
        {
            PlayerHealth.RestoreFullHealth();
            /*if(SpawnManager.instance)
            {
                SpawnManager.instance.RespawnSelf(this.gameObject);
                

            }else
            {
                Debug.LogWarning("SpawnManager not found, player won't respawn");
            }*/
        }
        public void RetrieveItem(GameObject item)
        {
            GameManager.Instance.GoalItemRetrieved(item);
        }

        public void UpdateObjective()
        {
            PlayerUI.UpdateObjective();
        }
/*        private void PlayerDies()
        {
            if (GameManager.Instance)
            {
                if (GameManager.Instance.respawnEnabled)
                {
                    Spawn();
                }else
                {
                    //No respawn logic
                }
            }else
            {
                Spawn();
            }

        }*/
        private void TestDmg()
        {
            if(true)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    TakeDamage(Random.Range(5, 20));
                } 
                if (Input.GetKeyDown(KeyCode.C))
                {
                    RestoreHealth(Random.Range(5, 20));
                }
            }
        }

        public void EnableCamera()
        {
            _cam.transform.gameObject.SetActive(true);
        }

        #endregion

    }

}

