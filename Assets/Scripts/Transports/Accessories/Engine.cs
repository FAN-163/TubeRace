using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    [SerializeField] private float mass = 15.0f;

    public float Mass { get => mass; set => mass = value; }
}
