using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMove : MonoBehaviour
{
    public GameObject bk;
    public GameObject panelPrincipal;
    Vector2 startPosition;
    Vector2 targetPosition;
    public bool isShow = false;
    public float speed;

    void Start() {

        startPosition = transform.position;
        targetPosition = bk.transform.position;
    }

    void Update() {
        var step = speed * Time.deltaTime;
        if (isShow) {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
        }

    }

    public void ChangeState() {

        // cambio el panel a mostrar
        isShow = !isShow;

        //chequeo si quedo un bk de btn activo y lo desactivo
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("btn_bk");
        foreach (GameObject element in gameObjects) {
            if (element.activeSelf) {
                element.SetActive(false);
            }
        }

        // verifico si mostrar o no el principal
        if (panelPrincipal.activeSelf) {
            panelPrincipal.SetActive(false);
        }
        else {
            panelPrincipal.SetActive(true);
        }
    }
}
