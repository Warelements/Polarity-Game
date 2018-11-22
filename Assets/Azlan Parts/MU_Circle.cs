using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class MU_Circle : MonoBehaviour
{
    [SerializeField]
    private int IN_Vertexcount=40;//vertexes in circle, more= smoother circle, less =more pixelated circle. Change in script as attached on runtime
    public float Fl_Radius;//radius of circle
    public LineRenderer lineRenderer;
    [SerializeField]
    private float Fl_LineWidth = 0.2f;//preset circle linewidth, change in script as attached on runtime
    [SerializeField]
    private Material LineMat;
    // Update is called once per frame
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>(); //accessor for line renderer component attached      
    }
    private void Start()
    {
        DrawCircle();
    }
    //draws a circle of a certain radius 
    void DrawCircle()
    {
        Fl_Radius = GetComponent<MU_ObjectProperties>().Fl_Range;//sets radius to the magnets range
        lineRenderer.loop = true;// loops line for a complete circle
        lineRenderer.widthMultiplier = Fl_LineWidth;//sets the width of the line
    //    Fl_Radius = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelRect.yMax, 0)), Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelRect.yMin, 0))) * 0.5f - Fl_LineWidth;
        float deltaTheta = (2 * Mathf.PI) / IN_Vertexcount;
        float Theta=0 ;
        lineRenderer.positionCount = IN_Vertexcount;
        for (int i = 0; i < lineRenderer.positionCount; i++)//for loop used to position of the lines to form a circle
        {
            Vector3 pos = new Vector3(transform.position.x+ Fl_Radius * Mathf.Cos(Theta),transform.position.y+ Fl_Radius * Mathf.Sin(Theta), 0);
            lineRenderer.SetPosition(i, pos);
            Theta += deltaTheta;
        }
        lineRenderer.textureMode = LineTextureMode.Tile;//sets the texture mode for the line to tile
    }
}
