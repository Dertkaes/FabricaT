using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FabricaT
{
    public class ResourceLine : MonoBehaviour
    {
        [SerializeField]
        private ResourceType _resourceType;

        [SerializeField]
        private TMP_Text _resourseText;

        [SerializeField]
        private TMP_Text _resourseAmountText;

        private void Start()
        {
            _resourseText.text = $"{_resourceType.ToString()}:";
        }

        private void Update()
        {

        }
        public void SetResourseAmount(ResourceType resourceType, int resourceIncrease, int amount)
        {
            if (_resourceType == resourceType)
            {
                _resourseAmountText.text = $"{resourceIncrease}+{amount}={resourceIncrease + amount}";
            }
        }
        public void SetResourseAmount(List<ResourceExtractioneeInfo> resourceExtractioneeInfos)
        {
            foreach (var resourceExtractioneeInfo in resourceExtractioneeInfos)
            {
                if (_resourceType == resourceExtractioneeInfo.ResourceType)
                {
                    _resourseAmountText.text = $"{resourceExtractioneeInfo.ResourceAmount}+{resourceExtractioneeInfo.ResourceExtractione}" +
                        $"={resourceExtractioneeInfo.ResourceAmount + resourceExtractioneeInfo.ResourceExtractione}";
                }
            }
        }
    }
}
