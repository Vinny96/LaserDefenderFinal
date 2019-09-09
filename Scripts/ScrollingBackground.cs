using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    // configuration parameters

    Material backGroundHandle = null;
    [SerializeField] float backGroundOffSet = 0;





    // Start is called before the first frame update
    void Start()
    {
        backGroundHandle = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        getBackGroundScrolling();
    }

    private void getBackGroundScrolling()
    {
        backGroundHandle.mainTextureOffset += new Vector2(0, backGroundOffSet) * Time.deltaTime;
    }
}
