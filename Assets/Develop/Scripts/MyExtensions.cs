namespace Develop.Scripts {
    public static class MyExtensions {
        public static ColorType Reverse(this ColorType _base) {
            return _base == ColorType.Black ? ColorType.White : ColorType.Black;
        }
    }
}