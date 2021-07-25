using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    MouseInput mouseInput;
    // Start is called before the first frame update
    void Start()
    {
        mouseInput.Mouse.MouseClick.performed += x =>  MouseClick(); 
    }

    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MouseClick()
    {
        Vector2 mpos = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mpos = Camera.main.ScreenToWorldPoint(mpos);
    }
}
