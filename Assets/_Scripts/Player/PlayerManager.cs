using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region Config
    [Header("Input Actions")]
    [SerializeField] private InputActionAsset actions;
    [Space(20)]
    [SerializeField] private GameObject lightLeft;
    [SerializeField] private GameObject lightRight;
    [SerializeField] private GameObject movingObject;
    
    [Header("Player Settings")]
    [SerializeField] private float speed;
    
    private InputAction _lightRight;
    private InputAction _lightLeft;
    #endregion
    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void Awake()
    {
        actions.FindAction("LightRight").performed += ctx => LightRight();
        actions.FindAction("LightLeft").performed += ctx => LightLeft();
    }

    private void FixedUpdate()
    {
        if (lightRight.activeSelf && lightLeft.activeSelf)
        {
            
        }
        else if (lightRight.activeSelf)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, lightRight.transform.position, speed*Time.fixedDeltaTime);
        }
        else if (lightLeft.activeSelf)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, lightLeft.transform.position, speed*Time.fixedDeltaTime);
        }
    }

    private void LightRight()
    {
        if (lightRight.activeSelf)
        {
            lightRight.SetActive(false);
        }
        else
        {
            lightRight.SetActive(true);
        }
        
    } // Checks if the object is active or not, and then sets it to the opposite.
    
    private void LightLeft()
    {
        if (lightLeft.activeSelf)
        {
            lightLeft.SetActive(false);
        }
        else
        {
            lightLeft.SetActive(true);
        }
    } // Checks if the object is active or not, and then sets it to the opposite.
}
