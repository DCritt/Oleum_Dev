using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCellDock : MonoBehaviour
{

    [SerializeField] private SpriteRenderer PowerCellDockSprite;
    [SerializeField] private Animator powerCellDockAnimator;
    private Coroutine docking;

    public IEnumerator dock()
    {

        Debug.Log(powerCellDockAnimator.GetCurrentAnimatorClipInfo(0).Length);

        yield return new WaitForSeconds(powerCellDockAnimator.GetCurrentAnimatorClipInfo(0).Length);

    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Hello");

        StartCoroutine("dock");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
