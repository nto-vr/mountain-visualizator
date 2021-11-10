using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Tester : MonoBehaviour
{
    public float Radius;
    public Vector3 CameraCoords;
    public Vector3 CameraLook;
    public Vector3 Up;
    public int N;
    public List<Vector4> ObjectsCoords;
    public GameObject CameraPrefab;
    public GameObject Mountain;
    public GameObject Light;
    private string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "input.txt");
        using (StreamReader sw = new StreamReader(path))
        {
            Radius = Convert.ToSingle(sw.ReadLine().Replace('.', ','));
            string r = sw.ReadLine();
            string[] C = r.Split(' ');
            // Меняем координаты y и z местами с учётом системы координат Unity
            CameraCoords.x = Convert.ToSingle(C[0].Replace('.', ','));
            CameraCoords.y = Convert.ToSingle(C[2].Replace('.', ','));
            CameraCoords.z = Convert.ToSingle(C[1].Replace('.', ','));
            r = sw.ReadLine();
            string[] L = r.Split(' ');
            CameraLook.x = Convert.ToSingle(L[0].Replace('.', ','));
            CameraLook.y = Convert.ToSingle(L[2].Replace('.', ','));
            CameraLook.z = Convert.ToSingle(L[1].Replace('.', ','));
            r = sw.ReadLine();
            string[] U = r.Split(' ');
            Up.x = Convert.ToSingle(U[0].Replace('.', ','));
            Up.y = Convert.ToSingle(U[2].Replace('.', ','));
            Up.z = Convert.ToSingle(U[1].Replace('.', ','));
            N = Convert.ToInt32(sw.ReadLine());
            for (int i = 0; i < N; i++)
            {
                string[] t = sw.ReadLine().Split(' ');
                Vector4 v = new Vector4(Convert.ToSingle(t[0].Replace('.', ',')), Convert.ToSingle(t[2].Replace('.', ',')), Convert.ToSingle(t[1].Replace('.', ',')), Convert.ToSingle(t[3].Replace('.', ',')));
                ObjectsCoords.Add(v);
            }
        }
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(Radius * 2, Radius * 2, Radius * 2);
        GameObject camera = Instantiate(CameraPrefab, CameraCoords, Quaternion.identity);
        Vector3 target = CameraCoords + CameraLook;
        camera.transform.LookAt(target, Up);
        Light.transform.eulerAngles = camera.transform.eulerAngles;
        for (int i = 0; i < N; i++)
        {
            GameObject mountain = Instantiate(Mountain, new Vector3(ObjectsCoords[i].x, ObjectsCoords[i].y, ObjectsCoords[i].z), Quaternion.identity);
            mountain.name = $"Mountain {(i + 1).ToString()}";
            mountain.transform.LookAt(new Vector3(ObjectsCoords[i].x * 2, ObjectsCoords[i].y * 2, ObjectsCoords[i].z * 2));
            mountain.transform.localScale = new Vector3(ObjectsCoords[i].w/2, ObjectsCoords[i].w/2, ObjectsCoords[i].w/2);
        }
    }
}
