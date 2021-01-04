using UnityEngine;

namespace Develop.Scripts {
    public static class BitExtensions {
        public static ColorType Reverse(this ColorType _base) {
            return _base == ColorType.Black ? ColorType.White : ColorType.Black;
        }

        public static void SetScale(this Transform _base, Vector3 _scale) {
            _base.localScale = _scale;
        }

        public static void SetActive(this Transform _base, bool _isActive) {
            _base.gameObject.SetActive(_isActive);
        }
    }
}