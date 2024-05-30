using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class Interaction : MonoBehaviour
{

    public float CheckRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask mlayerMask;

    public GameObject curlnteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;


    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > CheckRate)
        {
            lastCheckTime = Time.time;
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2) );
            RaycastHit hit;

            //Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red, 15);
            if (Physics.Raycast(ray, out hit, maxCheckDistance, mlayerMask))
            {
                if (hit.collider.gameObject != curlnteractGameObject)
                {
                    curlnteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curlnteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractionInput (InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && curInteractable != null) 
        {
            curInteractable.OnInteract();
            curlnteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
