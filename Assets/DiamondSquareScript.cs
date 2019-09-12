using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSquareScript : MonoBehaviour
{

    // Number of faces
    public int mDivisions;
    // Size of the terrain
    public float mSize;
    // Max height for terrain
    public float heightMap;

    // Vertices for terrain
    Vector3[]  mVerts;
    // Vertex counter
    int mVertCount;


    // Start is called before the first frame update
    void Start()
    {
        createTerrain();
    }

    void createTerrain() {
        
        // Array of width and height
        mVertCount = (mDivisions+1)*(mDivisions+1);
        // Initialize array
        mVerts = new Vector3[mVertCount];
        Vector2[] uvs = new Vector2[mVertCount];
        // Number of triangles
        int[] tris = new int[mDivisions*mDivisions*6];

        // Half the size of terrain
        float halfSize = mSize * 0.5f;
        // Size of a square
        float divisionSize = mSize/mDivisions;

        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        int triOffset = 0;

        for (int i = 0; i <= mDivisions; i++) {
            for (int j = 0; j <= mDivisions; j++) {
                // Vertex position
                mVerts[i*(mDivisions+1)+j] = new Vector3(-halfSize+j*divisionSize, 0.0f, halfSize-i*divisionSize);
                uvs[i*(mDivisions+1)+j] = new Vector2((float)i/mDivisions, (float)j/mDivisions);

                // Building triangles
                if (i < mDivisions && j < mDivisions) {
                    int topLeft = i*(mDivisions+1)+j;
                    int bottomLeft = (i+1)*(mDivisions+1)+j;

                    tris[triOffset] = topLeft;
                    tris[triOffset+1] = topLeft+1;
                    tris[triOffset+2] = bottomLeft+1;

                    tris[triOffset+3] = topLeft;
                    tris[triOffset+4] = bottomLeft+1;
                    tris[triOffset+5] = bottomLeft;

                    triOffset += 6;
                }
            }
        }

        mVerts[0].y = Random.Range(-heightMap * 1.0f, heightMap);
        mVerts[mDivisions].y = Random.Range(-heightMap * 0.5f, heightMap);
        mVerts[mVerts.Length-1].y = Random.Range(-heightMap * 0.5f, heightMap);
        mVerts[mVerts.Length-1-mDivisions].y = Random.Range(-heightMap * 0.5f, heightMap);

        int iterations = (int)Mathf.Log(mDivisions, 2);
        int numSquares = 1;
        int squareSize = mDivisions;
        
        for (int i = 0; i < iterations; i++) {
            int row = 0;
            for (int j = 0; j < numSquares; j++) {
                int col = 0;
                for (int k = 0; k < numSquares; k++) {

                    DiamondSquare(row, col, squareSize, heightMap);
                    col += squareSize;
                }
                row += squareSize;
            }
            numSquares *= 2;
            squareSize /= 2;
            heightMap *= 0.5f;
        }

        mesh.vertices = mVerts;
        mesh.uv = uvs;
        mesh.triangles = tris;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    void DiamondSquare(int row, int col, int size, float offset) {
        int halfSize = (int)(size*0.5f);
        int topLeft = row*(mDivisions+1)+col;
        int bottomLeft = (row+size)*(mDivisions+1)+col;

        int midPoint = (int)(row+halfSize)*(mDivisions+1)+(int)(col+halfSize);
        mVerts[midPoint].y = (mVerts[topLeft].y+mVerts[topLeft+size].y+mVerts[bottomLeft].y+mVerts[bottomLeft+size].y)*0.25f + Random.Range(-offset, offset);
    
        mVerts[topLeft+halfSize].y = (mVerts[topLeft].y+mVerts[topLeft+size].y+mVerts[midPoint].y)/ 3 + Random.Range(-offset, offset);
        mVerts[midPoint-halfSize].y = (mVerts[topLeft].y+mVerts[bottomLeft].y+mVerts[midPoint].y)/ 3 + Random.Range(-offset, offset);
        mVerts[midPoint+halfSize].y = (mVerts[topLeft+size].y+mVerts[bottomLeft+size].y+mVerts[midPoint].y)/ 3 + Random.Range(-offset, offset);
        mVerts[bottomLeft+halfSize].y = (mVerts[bottomLeft].y+mVerts[bottomLeft+size].y+mVerts[midPoint].y)/ 3 + Random.Range(-offset, offset);
    }
}
