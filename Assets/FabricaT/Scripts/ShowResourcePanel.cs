using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FabricaT
{
    public class ShowResourcePanel : MonoBehaviour
    {
        [SerializeField]
        private Button _okButton;
        [SerializeField]
        private List<ResourceLine> _resourceLines;
        [SerializeField]
        private CharacterController _characterController;

        private void Start()
        {
            gameObject.SetActive(false);
            _okButton.onClick.AddListener(() => 
            { 
                gameObject.SetActive(false);
                _characterController.ActivateControl.Invoke();
            });
        }

        public void SetResourseAmount(ResourceType resourceType, int resourceIncrease, int amount)
        {
            foreach (var resourceLine in _resourceLines)
            {
                resourceLine.SetResourseAmount(resourceType, resourceIncrease, amount);
            }
        }
        public void SetResourseAmount(List<ResourceExtractioneeInfo> resourceExtractioneeInfos)
        {
            foreach (var resourceLine in _resourceLines)
            {
                resourceLine.SetResourseAmount(resourceExtractioneeInfos);
            }
        }
    }
}
