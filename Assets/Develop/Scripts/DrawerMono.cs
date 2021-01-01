using UnityEngine;

namespace Develop.Scripts {
    public class DrawerMono : SingletonMono<DrawerMono> {

        [SerializeField] private MeshFilter mMesh;
        
        private CellData mBeforeCellData;

        private Vector2 mVertices;

        protected override void OnInstanceMade() {
            
        }

        public void Draw(ref CellData _newCellData) {

            mMesh.mesh.vertices
            mMesh.mesh      = ;
            mBeforeCellData = _newCellData;
        }
    }
}