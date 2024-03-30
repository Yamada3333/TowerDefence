using R3;
using UnityEngine;

namespace TowerDefence.Scripts.InGame.Model
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        
        [SerializeField] AudioClip bgm;
        [SerializeField] AudioClip spawnSound;
        [SerializeField] AudioClip deathSound;
        [SerializeField] AudioClip hitSound;
    
        public readonly Subject<Unit> OnSpawn = new();
        public readonly Subject<Unit> OnDeath = new();
        public readonly Subject<Unit> OnHit = new();
    
        private AudioSource _audioSource;
        
        private void Awake()
        {
            // シングルトン
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = 0.1f;
            
            // BGM再生
            _audioSource.clip = bgm;
            _audioSource.loop = true;
            _audioSource.Play();
            
            OnSpawn.Subscribe(_ => PlaySound(spawnSound)).AddTo(gameObject);
            OnDeath.Subscribe(_ => PlaySound(deathSound)).AddTo(gameObject);
            OnHit.Subscribe(_ => PlaySound(hitSound)).AddTo(gameObject);
        }
    
        private void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
    }
}
