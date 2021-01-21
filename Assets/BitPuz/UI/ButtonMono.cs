using System;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace BitPuz.UI {
    public class ButtonMono : MonoBehaviour {
        [SerializeField] private GameObject mNormalGameObject;
        [SerializeField] private GameObject mPressedGameObject;

        public  IObservable<Unit> OnPressed  => this.OnMouseDownAsObservable();
        private IObservable<Unit> OnReleased => this.OnMouseUpAsObservable();

        private void Awake() {
            OnPressed.Subscribe(_ => SetStatus(true)).AddTo(this);
            OnReleased.Subscribe(_ => SetStatus(false)).AddTo(this);
        }

        [Button("Set")]
        public void SetStatus(bool _isPressed) {
            mNormalGameObject.SetActive(!_isPressed);
            mPressedGameObject.SetActive(_isPressed);
        }
    }
}