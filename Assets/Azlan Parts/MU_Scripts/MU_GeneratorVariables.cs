
using UnityEngine;

public class MU_GeneratorVariables : MonoBehaviour
{
    public bool bl_Generator_On;
    public MU_Electromagnet Generator;
    private void Start()
    {
        
    }
    private void Update()
    {
        if(bl_Generator_On)
        {
            DrawLinetoTarget();
        }
        else if(bl_Generator_On==false&& GetComponent<LineRenderer>() != null)
        {
            Destroy(GetComponent<LineRenderer>());
        }
    }
    void DrawLinetoTarget()
    {
        if (GetComponent<LineRenderer>() == null)
        {
            LineRenderer Ln_Renderer = gameObject.AddComponent<LineRenderer>();
            Ln_Renderer.SetPosition(0, transform.position);
            Ln_Renderer.SetPosition(1, Generator.transform.position);
            Ln_Renderer.startWidth = 0.01f;
            Ln_Renderer.endWidth = 0.01f;
        }
    }
}
