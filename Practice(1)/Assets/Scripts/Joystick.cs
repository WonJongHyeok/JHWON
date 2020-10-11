using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform BackGround;
    public Transform player;
    Vector3 moveForeward;
    Vector3 moveRotate;
    public float moveSpeed;
    public float rotateSpeed;

    bool walking;

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        transform.localPosition =
            Vector2.ClampMagnitude(eventData.position - (Vector2)BackGround.position,BackGround.rect.width * 0.5f);

        moveForeward = new Vector3(0, 0, transform.localPosition.y).normalized;
        moveRotate = new Vector3(0, transform.localPosition.x, 0).normalized;

        if(!walking)
        {
            walking = true;
            player.GetComponent<Animator>().SetBool("Walk", true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine("PlayerMove");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        moveForeward = Vector3.zero;
        moveRotate = Vector3.zero;
        StopCoroutine("PlayerMove");

        walking = false;
        player.GetComponent<Animator>().SetBool("Walk", false);
    }
    IEnumerator PlayerMove()
    {
        while(true)
        {
            player.Translate(moveForeward * moveSpeed * Time.deltaTime);

            if (Mathf.Abs(transform.localPosition.x) > BackGround.rect.width * 0.3f)
                player.Rotate(moveRotate * rotateSpeed * Time.deltaTime);
            yield return null;
        }
    }
    
}
