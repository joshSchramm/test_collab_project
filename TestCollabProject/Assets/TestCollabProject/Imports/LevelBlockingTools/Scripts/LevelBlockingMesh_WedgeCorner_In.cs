using UnityEngine;
using System.Collections;

namespace LevelBlockingTools
{
    public class LevelBlockingMesh_WedgeCorner_In : LevelBlockingMesh
    {
        public override void UpdateMesh()
        {
            //Debug.Log("updating mesh - " + meshFilter.sharedMesh.name);
            meshFilter.sharedMesh.uv = origUVs;
            Vector2[] uvs = meshFilter.sharedMesh.uv;

            // apply scale
            for(int i = 0; i < 12; i += 3)
            {
                if(i == 9) // right
                {
                    uvs[i + 1].y = transform.localScale.y * UVScale.y;
                    uvs[i + 1].x = transform.localScale.z * UVScale.x;
                    uvs[i + 2].x = transform.localScale.z * UVScale.x;
                }
                else if(i == 6) // top 2
                {
                    uvs[i].x = ((transform.localScale.z + transform.localScale.x) / 4) * UVScale.x;
                    uvs[i + 1].x = ((transform.localScale.z + transform.localScale.x) / 4) * UVScale.x;
                    uvs[i + 1].y = ((transform.localScale.y + transform.localScale.x + transform.localScale.z) / 3) * UVScale.y;
                    uvs[i + 2].x = ((transform.localScale.z + transform.localScale.x) / 2) * UVScale.x;
                    uvs[i + 2].y = ((transform.localScale.y + transform.localScale.z) / 2) * UVScale.y;
                }
                else if(i == 3) // top 1
                {
                    uvs[i].x = ((transform.localScale.z + transform.localScale.x) / 4) * UVScale.x;
                    uvs[i + 1].y = ((transform.localScale.y + transform.localScale.x) / 2) * UVScale.y;    
                    uvs[i + 2].x = ((transform.localScale.z + transform.localScale.x) / 4) * UVScale.x;
                    uvs[i + 2].y = ((transform.localScale.y + transform.localScale.x + transform.localScale.z) / 3) * UVScale.y;
                }
                else if (i == 0) // front
                {
                    uvs[i + 1].y = transform.localScale.y * UVScale.y;
                    uvs[i + 2].x = transform.localScale.x * UVScale.x;
                }
            }

            // apply scale
            for(int i = 12; i < 24; i += 4)
            {
                if(i == 20) // bottom
                {
                    uvs[i].y = transform.localScale.z * UVScale.y * -1;
                    uvs[i + 2].x = transform.localScale.x * UVScale.x;
                    uvs[i + 3].x = transform.localScale.x * UVScale.x;
                    uvs[i + 3].y = transform.localScale.z * UVScale.y * -1;
                }
                else if(i == 16) // left
                {
                    uvs[i].x = transform.localScale.z * UVScale.x * -1;
                    uvs[i + 1].x = transform.localScale.z * UVScale.x * -1;
                    uvs[i + 1].y = transform.localScale.y * UVScale.y;
                    uvs[i + 2].y = transform.localScale.y * UVScale.y;
                }
                else if(i == 12)// back
                {
                    uvs[i].x = transform.localScale.x * UVScale.x * -1;
                    uvs[i + 1].x = transform.localScale.x * UVScale.x * -1;
                    uvs[i + 1].y = transform.localScale.y * UVScale.y;
                    uvs[i + 2].y = transform.localScale.y * UVScale.y;
                }
            }

            // uv positions per face
            //  1       2
            //
            //  0       3

            // apply pos
            for(int i = 0; i < uvs.Length; i++)
            {
                uvs[i] += UVPos;
            }

            meshFilter.sharedMesh.uv = uvs;
            lastScale = transform.localScale;
            lastUVScale = UVScale;
            lastUVPos = UVPos;
        }

        public override void CreateMesh()
        {
            Debug.Log("Creating WedgeCorner_In");

            // initial hardcoded sizes for a cube
            Vector3[] verts = new Vector3[24];
            Vector2[] uvs = new Vector2[24];
            int[] tris = new int[30];

            // set vert positions per face
            //	1		2
            //	
            //	0		3
            //
            // front
            verts[0] = new Vector3(0, 0, 0);
            verts[1] = new Vector3(0, 1, 0);
            verts[2] = new Vector3(1, 0, 0);
            // top 1
            verts[3] = new Vector3(1, 0, 0);
            verts[4] = new Vector3(0, 1, 0);
            verts[5] = new Vector3(0, 1, 1);
            // top 2
            verts[6] = new Vector3(1, 0, 0);
            verts[7] = new Vector3(0, 1, 1);
            verts[8] = new Vector3(1, 1, 1);
            // right
            verts[9] = new Vector3(1, 0, 0);
            verts[10] = new Vector3(1, 1, 1);
            verts[11] = new Vector3(1, 0, 1);
            // back
            verts[12] = new Vector3(1, 0, 1);
            verts[13] = new Vector3(1, 1, 1);
            verts[14] = new Vector3(0, 1, 1);
            verts[15] = new Vector3(0, 0, 1);
            // left
            verts[16] = new Vector3(0, 0, 1);
            verts[17] = new Vector3(0, 1, 1);
            verts[18] = new Vector3(0, 1, 0);
            verts[19] = new Vector3(0, 0, 0);
            // bottom
            verts[20] = new Vector3(0, 0, 1);
            verts[21] = new Vector3(0, 0, 0);
            verts[22] = new Vector3(1, 0, 0);
            verts[23] = new Vector3(1, 0, 1);

            // set uv positions per face
            //	1		2
            //
            //	0		3
            // front
            uvs[0] = new Vector2(0, 0);
            uvs[1] = new Vector2(0, 1);
            uvs[2] = new Vector2(1, 0);

            // top 1
            uvs[3] = new Vector2(0.5f, 0);
            uvs[4] = new Vector2(0, 1);
            uvs[5] = new Vector2(0.5f, 1);
            // top 2
            uvs[6] = new Vector2(0.5f, 0);
            uvs[7] = new Vector2(0.5f, 1);
            uvs[8] = new Vector2(1, 1);

            // right
            uvs[9] = new Vector2(0, 0);
            uvs[10] = new Vector2(1, 1);
            uvs[11] = new Vector2(1, 0);

            //back face
            uvs[12] = new Vector2(-1, 0);
            uvs[13] = new Vector2(-1, 1);
            uvs[14] = new Vector2(0, 1);
            uvs[15] = new Vector2(0, 0);

            //left face
            uvs[16] = new Vector2(-1, 0);
            uvs[17] = new Vector2(-1, 1);
            uvs[18] = new Vector2(0, 1);
            uvs[19] = new Vector2(0, 0);

            // bottom face
            uvs[20] = new Vector2(0, -1);
            uvs[21] = new Vector2(0, 0);
            uvs[22] = new Vector2(1, 0);
            uvs[23] = new Vector2(1, -1);

            // set triangle array per face
            int anchorIndex = 0;
            for(int i = 0; i < 12; i += 3)
            {
                tris[i] = anchorIndex;
                tris[i + 1] = anchorIndex + 1;
                tris[i + 2] = anchorIndex + 2;
                anchorIndex += 3;
            }

            for(int i = 12; i < 30; i += 6)
            {
                tris[i] = anchorIndex;
                tris[i + 1] = anchorIndex + 1;
                tris[i + 2] = anchorIndex + 2;
                tris[i + 3] = anchorIndex;
                tris[i + 4] = anchorIndex + 2;
                tris[i + 5] = anchorIndex + 3;

                anchorIndex += 4;
            }

            // assing data to the mesh
            meshFilter.sharedMesh = new Mesh();
            meshFilter.sharedMesh.vertices = verts;
            meshFilter.sharedMesh.uv = uvs;
            meshFilter.sharedMesh.triangles = tris;
            meshFilter.sharedMesh.RecalculateNormals();
            meshFilter.sharedMesh.RecalculateBounds();
            meshFilter.sharedMesh.name = "LBM_WedgeCorner_In_" + transform.GetInstanceID();
#if UNITY_EDITOR
            SecondaryUV(meshFilter.sharedMesh);
            TangentSolver.Solve(meshFilter.sharedMesh);
#endif
            origUVs = meshFilter.sharedMesh.uv;
        }
    }
}
