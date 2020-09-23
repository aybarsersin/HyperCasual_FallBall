using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] float jumpForce;
    int chainCombo=0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Score")
        {
            GameManager.instance.AddScore(5);
            Destroy(other.transform.parent.gameObject);
            chainCombo++;
            UpdateCahin();
        }
    }

    void UpdateCahin()
    {
        GameManager.instance.chainText.text = ""+chainCombo;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (chainCombo<=2)
        {
            if (collision.transform.tag == "Safe")
            {
                rb.velocity = Vector3.up * jumpForce;
                chainCombo = 0;
                UpdateCahin();

            }
            else if (collision.transform.tag == "Danger")
            {
                GameManager.instance.SaveBeforeDeath();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            chainCombo = 0;
                UpdateCahin();

            }
        }
        else
        {
            GameManager.instance.AddScore(5);
            Destroy(collision.transform.parent.gameObject);
            chainCombo = 0;
            UpdateCahin();
        }

    }
}
