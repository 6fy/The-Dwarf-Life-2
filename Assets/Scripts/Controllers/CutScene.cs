using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CutScene : MonoBehaviour
{
    public float moveSpeed = 60.0f;
    public float cameraSpeed = 3.5f;

    public Transform target;
    public new Camera camera;

    public GameObject canvas;

    private Vector3 flyCameraTowards;
    private SFX sfx;

    void Start()
    {
        canvas.SetActive(false);
        sfx = FindObjectOfType<SFX>();
        sfx.Play(SoundType.Meteor);
        flyCameraTowards = new Vector3(-16, 47, -93);
    }

    void Update()
    {
        MoveMeteorite();

        // wait for meteoride to reach target
        if (Time.time > 1) {
            MoveCamera();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            canvas.SetActive(true);
            LoadMenu();
        }
    }

    private void LoadMenu()
    {
        StartCoroutine(LoadMenuAfterDelay());
    }

    private IEnumerator LoadMenuAfterDelay()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Menu");
    }

    private void MoveMeteorite()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, (moveSpeed * Time.deltaTime));
    }

    private void MoveCamera()
    {
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, flyCameraTowards, (cameraSpeed * Time.deltaTime));
        LookAtTarget();
    }
   
    private void LookAtTarget()
    {
        Vector3 targetDir = target.position - transform.position;
        float step = 5 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
