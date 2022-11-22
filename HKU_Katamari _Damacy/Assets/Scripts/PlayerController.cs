using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float rollSpeed = 3f;
    [SerializeField] private float sizeAdjustment = 2;

    private float _size = 1f;
    private float _sizeUp = 0.1f;
    private float _sizeDown = -0.1f;

    private int _pointCounter;

    private Rigidbody _rigidbody;
    private PlayerInputActions _playerInputActions;
    private GameObject _lastTouchedObject;



    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();

        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Movement.Enable();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    private void OnMove()
    {
        Vector2 inputVector = _playerInputActions.Movement.Move.ReadValue<Vector2>();
        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        _rigidbody.AddForce(moveVector * rollSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Logic for hitting a Good interactable
        if (collision.gameObject.CompareTag("Pick-Up"))
        {
            //Size up the ball itself
            transform.localScale = transform.localScale * (1f + _sizeUp);
            _size += _sizeUp;

            //Deactivate all logic inside the object
            collision.gameObject.GetComponent<Interactable>().enabled = false;
            collision.collider.enabled = false;

            //Set the Object to stick to the ball
            collision.transform.SetParent(transform);
            _pointCounter += 1;
        }

        //Logic for hitting a bad interactable
        if (collision.gameObject.CompareTag("Auwie"))
        {
            //Size down the ball itself
            transform.localScale = transform.localScale * (1f + _sizeDown);
            _size -= _sizeDown;

            //Deactivate the object itself 
            collision.gameObject.SetActive(false);
        }
    }
}
