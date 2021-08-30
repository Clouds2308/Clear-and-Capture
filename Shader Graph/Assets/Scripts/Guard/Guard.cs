using UnityEngine;

public class Guard : MonoBehaviour,IDamageable
{
    [SerializeField] private int _currGuardHealth = 50;    //guard current health
    [SerializeField] private int _maxGuardHealth = 50;     //guard maximum health

    private Animator _guardAnimator;

    private bool _isGuardDead;
    public bool IsGuardDead { get => _isGuardDead; private set => _isGuardDead = value; }

    private void Start()
    {
        _guardAnimator = GetComponent<Animator>();
        _currGuardHealth = _maxGuardHealth;
    }

    public void TakeDamage(float damage)
    {
        _currGuardHealth -= (int)damage;

        if (_currGuardHealth <= 0f)
        {
            HandleGuardDeath();            
        }
    }

    private void HandleGuardDeath()
    {
        IsGuardDead = true;
        _guardAnimator.SetTrigger("IsDie");
        Destroy(this.gameObject,3.4f);
    }

}