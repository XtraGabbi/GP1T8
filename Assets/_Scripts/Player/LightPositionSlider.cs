using UnityEngine;
using UnityEngine.InputSystem;

public class LightPositionSlider : MonoBehaviour
{
    #region Config
    private float _maxHight;
    private float _maxWidth;
    float _currentHight;
    Camera _mainCamera;
    private float _lightPlacementRight;
    private float _lightPlacementLeft;

    [Header("Pacement Settings")] 
    [SerializeField] private bool placeItLeft;
    [SerializeField] private bool placeItRight;
    [SerializeField] private int placementRangeFromCamera;
    
    [Header("Input Actions")]
    [SerializeField] private InputActionAsset actions;
    #endregion
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _maxHight = Screen.height;
        _maxWidth = Screen.width;
        
        actions.FindAction("sliderRight").performed += MoveRightSlider;
        actions.FindAction("sliderLeft").performed += MoveLeftSlider;
    }
    // Update is called once per frame
    void Update()
    {
        if (placeItLeft)
        {
            _currentHight = Mathf.Lerp(0f, _maxHight, _lightPlacementLeft);
            LightPlacement(_currentHight, 0);
            
        }
        else if (placeItRight)
        {
            _currentHight = Mathf.Lerp(0f, _maxHight, _lightPlacementRight); // updates the float value of the current hight, based on how much you press the trigger.
            LightPlacement(_currentHight, _maxWidth); // using _currentHight and _maxWidth, to move the object in the world space.
        }
    }

    void LightPlacement(float hight, float width)
    {
        gameObject.transform.position = _mainCamera.ScreenToWorldPoint(new Vector3(width, hight, placementRangeFromCamera));
    } // Takes the hight and width and places the object in the world space BASED on the camera.
    
    void MoveRightSlider(InputAction.CallbackContext context)
    {
        _lightPlacementRight = context.ReadValue<float>();
    } // Gets input from the right controller trigger and gives a float value of 0-1.
    
    void MoveLeftSlider(InputAction.CallbackContext context)
    {
        _lightPlacementLeft = context.ReadValue<float>();
    } // Gets input from the left controller trigger and gives a float value of 0-1.
}
