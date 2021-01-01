namespace Develop.Scripts {
    public readonly struct CellData {
        private readonly bool[,] mBits;
        
        public CellData(bool _tmp=false) {
            mBits = new bool[8,8];
        }

        public void Set(int _x, int _y, bool _tf) {
            mBits[_x%8, _y%8] = _tf;
        }

        public bool Get(int _x, int _y) {
            return mBits[_x % 8, _y % 8];
        }
    }
}