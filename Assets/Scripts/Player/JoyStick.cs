using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    //[SerializeField]
    //private RectTransform _joyField;
    //[SerializeField]
    //private CharacterController player;
    //[SerializeField]
    //private Transform plTransf;
    //Vector3 move;
    //float speed = 3f;
    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.position = eventData.position;
    //    transform.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)_joyField.position, _joyField.rect.width * 0.5f);

    //    move = new Vector3(transform.localPosition.x, 0, transform.localPosition.y).normalized;

    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    StartCoroutine("PlayerMove");
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    transform.localPosition = Vector3.zero;
    //    move = Vector3.zero;
    //    StopCoroutine("PlayerMove");
    //}

    //IEnumerator PlayerMove()
    //{
    //    while (true)
    //    {
    //        player.Move(move * speed * Time.deltaTime);

    //        if (move != Vector3.zero)
    //            plTransf.rotation = Quaternion.Slerp(plTransf.rotation, Quaternion.LookRotation(move), 5 * Time.deltaTime);
    //        yield return null;
    //    }
    //}
}
