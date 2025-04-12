using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FabricaT
{
    public class SoundPanel : MonoBehaviour
    {
        private const string SOUND = "Sound";

        [SerializeField]
        private Button _soundButton;
        [SerializeField]
        private TMP_Text _soundText;

        private bool _isSoundOn = true;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _isSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt(SOUND, 1));
            _soundText.text = _isSoundOn ? "Sound On" : "Sound Off";

            _soundButton.onClick.AddListener(() =>
            {
                _isSoundOn = !_isSoundOn;
                PlayerPrefs.SetInt(SOUND, Convert.ToInt32(_isSoundOn));
                _soundText.text = _isSoundOn ? "Sound On" : "Sound Off";
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
