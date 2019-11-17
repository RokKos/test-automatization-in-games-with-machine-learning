using UnityEngine;

namespace IAmHere.Game
{
    public class SoundWaveController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody_ = null;
        [SerializeField] private CircleCollider2D collider_ = null;
        [SerializeField] private TrailRenderer TrailRenderer = null;

        private WorldEntityController _originEntity;
        private float _maxTimeAlive = 0;
        private float _timeAlive = 0.0f;

        public void Init(WorldEntityController originEntity, float maxTimeAlive)
        {
            _originEntity = originEntity;
            _maxTimeAlive = maxTimeAlive;
        }

        public Rigidbody2D GetRigidbody()
        {
            return rigidbody_;
        }

        public CircleCollider2D GetCircleCollider()
        {
            return collider_;

        }

        private void Start()
        {
            _timeAlive = 0.0f;
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;
            if (_timeAlive > _maxTimeAlive)
            {
                // TODO(Rok Kos): Pooling
                Destroy(this.gameObject);
            }
        }

        public void SetLineColor(Color startColor)
        {
            TrailRenderer.startColor = startColor;
        }


    }
}