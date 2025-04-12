using TMPro;
using UnityEngine;

namespace FabricaT
{
    public enum ResourceType
    {
        Wood,
        Stone,
        Iron,
        Gold,
        Diamond
    }
    public struct ResourceInfo
    {
        public ResourceType ResourceType;
        public int ResourceExtractione;
    }

    public struct ResourceExtractioneeInfo
    {
        public ResourceType ResourceType;
        public int ResourceAmount;
        public int ResourceExtractione;
    }

    public class Fabrica : MonoBehaviour
    {
        [SerializeField]
        private ResourceType _resourceType;
        [SerializeField]
        private int _resourceAmount;
        [SerializeField]
        private int _resourceExtractione = 4;

        [SerializeField]
        private TMP_Text _resourseText;

        private void Start()
        {
            _resourseText.text = $"{_resourceType.ToString()}:\n {_resourceAmount}";
        }

        public ResourceInfo GetResource()
        {
            _resourceAmount -= _resourceExtractione;
            _resourseText.text = $"{_resourceType.ToString()}:\n {_resourceAmount}";
            return new ResourceInfo
            {
                ResourceType = _resourceType,
                ResourceExtractione = _resourceExtractione
            };
        }
    }
}
