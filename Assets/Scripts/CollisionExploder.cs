using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionExploder : MonoBehaviour
{
    public int cubePerAxis = 8;
        public float delay = 1f;
    public float force = 300f;
    public float radius = 2f;

    // Start is called before the first frame update
    void Start()
    {
        invoke("Main", delay);
    }

    void Main()
    {
        for (int x = 0; x < cubePerAxis; x++)
        {
            for (int y = 0; y < cubePerAxis; y++)
            {
                for(int z =0; z<cubePerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }
        Destroy(gameObject);
    }

    void CreateCube(Vector3 coordinates)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer rd = cube.getComponent<Renderer>();
        rd.material = getComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / cubePerAxis;

        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transformlocalScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform..localScale);
        rb.AddExplosionForce(force, transform.position, radius);

    }
}
