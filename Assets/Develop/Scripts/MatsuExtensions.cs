namespace Develop.Scripts {
    public static class MatsuExtensions {
        public static int Surplus(this int _a, int _b) {
            if (_b < 0) _b = -_b;
            if (_a >= 0) return _a % _b;
            return _b + _a        % _b;
        }
    }
}