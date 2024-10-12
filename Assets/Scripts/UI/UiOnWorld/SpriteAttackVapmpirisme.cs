using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAttackVapmpirisme : MonoBehaviour
{
    [SerializeField] private AttackVapmirisme _attack;

    private SpriteRenderer _render;
    private Color _enableColor;
    private Color _disableColor;
    private Color _coldownColor;
    private int _dividerAlphaColorForView = 4;
    private float _earlierOffValue = 1f;


    private void Awake()
    {
        _render = GetComponent<SpriteRenderer>();
        _disableColor = Color.clear;
        _coldownColor = Color.blue;
        _enableColor = Color.red;
        _render.color = _disableColor;
    }

    private void OnEnable()
    {
        _attack.IsSpesialAttackIsRuning += AttackIsRun;
    }

    private void OnDisable()
    {
        _attack.IsSpesialAttackIsRuning -= AttackIsRun;
    }

    private void AttackIsRun(float timer)
    {
        _enableColor.a = (timer / _attack.TimeAttack / _dividerAlphaColorForView);

        if (timer <= 0)
        {
            _enableColor = Color.red;
            return;
        }

        if (timer < _attack.TimeAttack - _earlierOffValue)
        {
            _render.color = _enableColor;
        }
        else if (timer > _attack.TimeAttack - _earlierOffValue)
        {
            _enableColor = _coldownColor;
        }
        else
        {
            _render.color = _disableColor;
        }
    }
}
