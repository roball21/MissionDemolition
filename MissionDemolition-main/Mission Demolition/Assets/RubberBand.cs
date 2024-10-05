using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBand : MonoBehaviour
{
     public Transform slingshotStart;  
    public Transform projectile;      
    private LineRenderer lineRenderer;
    private bool isDragging = false;
    private Vector3 originalPos;
    private AudioSource audioSource;

    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;  
        originalPos = projectile.position;
        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if (slingshotStart != null && projectile != null) {
            if (isDragging) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;  
                projectile.position = mousePos;  // Update projectile position
            }

            lineRenderer.SetPosition(0, slingshotStart.position);
            lineRenderer.SetPosition(1, projectile.position);
        }
    }

    void OnMouseDown() {
        isDragging = true;
    }

    void OnMouseUp() {
        isDragging = false;
        ResetProjectile();
    }

    void ResetProjectile() {
        PlaySound();
        projectile.position = originalPos;
        lineRenderer.SetPosition(1, originalPos);
    }

    void PlaySound() {
        if (audioSource != null && audioSource.clip != null) {
            audioSource.Play();
        }
    }
}
