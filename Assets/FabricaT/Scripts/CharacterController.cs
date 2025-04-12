using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace FabricaT
{
    public class CharacterController : MonoBehaviour
    {
        private const string IDLE = "Idle";
        private const string WALK = "Walk";

        private @CustomAction _input;

        private NavMeshAgent _agent;
        private Animator _animator;

        [Header("Movement")]
        [SerializeField]
        private ParticleSystem _clickEffect;
        [SerializeField]
        private LayerMask _clickableLayers;

        [SerializeField]
        private Fabrica _target;

        [Header("ResourceExtraction")]
        [SerializeField]
        private float _resourceExtractionDistance = 1.5f;
        private int _amountWood = 0;
        private int _amountStone = 0;
        private int _amountIron = 0;
        private int _amountGold = 0;
        private int _amountDiamond = 0;

        [SerializeField]
        ShowResourcePanel _showResourcePanel;

        private float _lookRotationSpeed = 8f;

        public UnityEvent ActivateControl;

        private void Awake()
        {
            if (ActivateControl == null)
            {
                ActivateControl = new UnityEvent();
            }

            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();

            _input = new @CustomAction();
            AssignInputs();

            ActivateControl.AddListener(() => { _clickableLayers.value = -1; });
        }

        private void AssignInputs()
        {
            _input.Main.Move.performed += ctx => ClickToMove();
        }

        private void ClickToMove()
        {
            RaycastHit hit;
#if UNITY_ANDROID
            var positionClick = Touchscreen.current.position;
#else
            var positionClick = Mouse.current.position;
#endif
            if (Physics.Raycast(Camera.main.ScreenPointToRay(positionClick.value), out hit, 100, _clickableLayers))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    _target = hit.transform.GetComponent<Fabrica>();
                    if (_clickEffect != null)
                    { Instantiate(_clickEffect, hit.transform.position + new Vector3(0, 0.1f, 0), _clickEffect.transform.rotation); }
                }
                else
                {
                    _target = null;
                    if (_clickEffect != null)
                    { Instantiate(_clickEffect, hit.point + new Vector3(0, 0.1f, 0), _clickEffect.transform.rotation); }
                }
                _agent.destination = hit.point;
            }
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            FollowTarget();
            FaceTarget();
            SetAnimations();
        }

        private void FollowTarget()
        {
            if (_target == null) return;
            if (Vector3.Distance(_target.transform.position, transform.position) <= _resourceExtractionDistance)
            { 
                ReachDistance(); 
            }
        }

        private void FaceTarget()
        {
            Vector3 direction = (_agent.destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _lookRotationSpeed);
        }

        private void SetAnimations()
        {
            if (_agent.velocity == Vector3.zero)
            { _animator.Play(IDLE); }
            else
            { _animator.Play(WALK); }
        }

        private void ReachDistance()
        {
            _agent.SetDestination(transform.position);
            var extractedResources = _target.GetResource();
            var ResourceExtractioneeInfo = new List<ResourceExtractioneeInfo>();
            switch (extractedResources.ResourceType)
            {
                case ResourceType.Wood:
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Wood,
                        ResourceAmount = _amountWood,
                        ResourceExtractione = extractedResources.ResourceExtractione
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Stone,
                        ResourceAmount = _amountStone,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Iron,
                        ResourceAmount = _amountIron,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Gold,
                        ResourceAmount = _amountGold,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Diamond,
                        ResourceAmount = _amountDiamond,
                        ResourceExtractione = 0
                    });
                    _amountWood += extractedResources.ResourceExtractione;
                    break;
                case ResourceType.Stone:
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Wood,
                        ResourceAmount = _amountWood,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Stone,
                        ResourceAmount = _amountStone,
                        ResourceExtractione = extractedResources.ResourceExtractione
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Iron,
                        ResourceAmount = _amountIron,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Gold,
                        ResourceAmount = _amountGold,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Diamond,
                        ResourceAmount = _amountDiamond,
                        ResourceExtractione = 0
                    });
                    _amountStone += extractedResources.ResourceExtractione;

                    break;
                case ResourceType.Iron:
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Wood,
                        ResourceAmount = _amountWood,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Stone,
                        ResourceAmount = _amountStone,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Iron,
                        ResourceAmount = _amountIron,
                        ResourceExtractione = extractedResources.ResourceExtractione
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Gold,
                        ResourceAmount = _amountGold,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Diamond,
                        ResourceAmount = _amountDiamond,
                        ResourceExtractione = 0
                    });
                    _amountIron += extractedResources.ResourceExtractione;
                    break;
                case ResourceType.Gold:
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Wood,
                        ResourceAmount = _amountWood,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Stone,
                        ResourceAmount = _amountStone,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Iron,
                        ResourceAmount = _amountIron,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Gold,
                        ResourceAmount = _amountGold,
                        ResourceExtractione = extractedResources.ResourceExtractione
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Diamond,
                        ResourceAmount = _amountDiamond,
                        ResourceExtractione = 0
                    });
                    _amountGold += extractedResources.ResourceExtractione;
                    break;
                case ResourceType.Diamond:
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Wood,
                        ResourceAmount = _amountWood,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Stone,
                        ResourceAmount = _amountStone,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Iron,
                        ResourceAmount = _amountIron,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Gold,
                        ResourceAmount = _amountGold,
                        ResourceExtractione = 0
                    });
                    ResourceExtractioneeInfo.Add(new ResourceExtractioneeInfo
                    {
                        ResourceType = ResourceType.Diamond,
                        ResourceAmount = _amountDiamond,
                        ResourceExtractione = extractedResources.ResourceExtractione
                    });
                    _amountDiamond += extractedResources.ResourceExtractione;
                    break;
            }
            _showResourcePanel.SetResourseAmount(ResourceExtractioneeInfo);
            _showResourcePanel.gameObject.SetActive(true);
            _clickableLayers.value= 0;
            _target = null;
        }
    }
}