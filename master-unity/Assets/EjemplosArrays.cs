using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjemplosArrays : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] nums1 = { 1, 2, 3, 4, 5 };
        int[,] nums2 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

        int[][] matrizEscalonada = {
            new int[] {1, 2, 3},
            new int[] {4, 5},
            new int[] {6, 7, 8, 9}
        };

        for (int i = 0; i < matrizEscalonada.Length; i++)
        {
            for (int j = 0; j < matrizEscalonada[i].Length; j++)
            {
                Debug.Log($"matrizEscalonada[{i}][{j}]={matrizEscalonada[i][j]}");
            }
        }

        int[] nums = new int[5];
        nums[2] = 5;
        Debug.Log(string.Join(", ", nums)); // 0, 0, 5, 0, 0
        ModificarArray(nums);
        Debug.Log(string.Join(", ", nums)); // 0, 1, 7, 3, 4
        ModificarArray(nums);
        Debug.Log(string.Join(", ", nums));	// 0, 2, 9, 6, 8

    }
    void ModificarArray(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++) arr[i] += i;
    }


    private void MostrarArray(int[] nums)
    {

    }

}
