using UnityEngine;

namespace Develop.Scripts {
    public static class BitExtensions {
        public static LightType Reverse(this LightType _base) {
            return _base == LightType.Off ? LightType.On : LightType.Off;
        }

        public static void SetScale(this Transform _base, Vector3 _scale) {
            _base.localScale = _scale;
        }

        public static void SetActive(this Transform _base, bool _isActive) {
            _base.gameObject.SetActive(_isActive);
        }
    }
}