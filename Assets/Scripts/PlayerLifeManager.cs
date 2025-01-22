using UnityEngine;
using UnityEngine.Events;

public class PlayerLifeManager : MonoBehaviour
{

    [SerializeField]
    private float _lifeImageWidth = 50.75f;

    [SerializeField]
    private int _playerMaximumLives = 3;

    [SerializeField]
    private int _currentLife =3;

    private RectTransform _rect;

    public UnityEvent OutOfLives;

    public int CurrentLife
    {
        get => _currentLife;

        private set
        {
            //invoke event OutOfLives
            if (value < 0)
            {
                OutOfLives?.Invoke();
            }
            //keep number of lives between 0 and the maximum
            _currentLife = Mathf.Clamp(value, 0, _playerMaximumLives);
            AdjustImageWidth();

        }
    }

    private void AdjustImageWidth()
    {
        _rect.sizeDelta = new Vector2(_lifeImageWidth * _currentLife, _rect.sizeDelta.y);
    }
    private void Awake()
    {
        _rect = transform as RectTransform;
        AdjustImageWidth();
    }

    //interact with life
    public void AddLife(int num = 1)
    {

    }

    public void RemoveLife(int num = 1)
    {

    }
}
