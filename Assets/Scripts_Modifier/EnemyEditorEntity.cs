#if UNITY_EDITOR
using UnityEngine;

namespace Phantom.Modifier {

    public class EnemyEditorEntity : MonoBehaviour {

        [SerializeField] RoleTM roleTM;

        public void Rename() {
            this.gameObject.name = $"EnemyEditor";
        }

        public RoleTM GetRoleTM() {
            return roleTM;
        }

        public Vector2 GetPos() {
            return transform.position;
        }

    }

}
#endif