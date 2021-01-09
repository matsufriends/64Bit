using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Develop.Scripts {
    public class StageMono : MonoBehaviour {
        [SerializeField] private ButtonMono     mStageOnButton;
        [SerializeField] private ButtonMono     mStageOffButton;
        [SerializeField] private List<TextMesh> mStageNumList;

        public ButtonMono OnButton  => mStageOnButton;
        public ButtonMono OffButton => mStageOffButton;
        
        [Button("Set")]
        public void Set(int _num, bool _lock) {
            mStageOnButton.gameObject.SetActive(!_lock);
            mStageOffButton.gameObject.SetActive(_lock);

            if ((mStageNumList?.Count ?? 0) == 0) return;
            foreach (var text in mStageNumList) {
                text.text = _num.ToString();
            }
        }
    }
}