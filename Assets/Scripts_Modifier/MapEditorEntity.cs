#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using MortiseFrame.Compass.Extension;
using TriInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom.Modifier {

    public class MapEditorEntity : MonoBehaviour {

        [SerializeField] int typeID;
        [SerializeField] GameObject mapSize;
        [SerializeField] MapTM mapTM;
        [SerializeField] Transform roleGroup;
        [SerializeField] Transform spawnPoint;
        [SerializeField] Transform obstacleRoot;
        [SerializeField] float gridUnit = 1;


        [Button("Bake")]
        void Bake() {
            BakeMapInfo();
            BakeSpawnPoint();
            BakeObstacle();
            EditorUtility.SetDirty(mapTM);
            AssetDatabase.SaveAssets();
            Debug.Log("Bake Sucess");
        }

        void BakeMapInfo() {
            mapTM.typeID = typeID;
            mapTM.mapSize = mapSize.transform.localScale.RoundToVector2Int();
        }

        void BakeObstacle() {
            PathFindingBakerHelper.Bake(obstacleRoot, -mapTM.mapSize / 2, mapTM.mapSize / 2, gridUnit, 0.0001f, out mapTM.obstacleData, out mapTM.obstacleDataWidth);
        }

        void BakeSpawnPoint() {
            var editor = spawnPoint.GetComponent<SpawnPointEditorEntity>();
            if (editor == null) {
                Debug.Log("SpawnPointEditor Not Found");
            }
            editor.Rename();
            mapTM.ownerSpawnPoint = editor.GetPos();
        }

        void OnDrawGrid() {
            PathFindingGizmosHelper.OnDrawGrid(gridUnit, -mapTM.mapSize / 2, mapTM.mapSize / 2);
        }

        void OnDrawObstacle() {
            PathFindingGizmosHelper.OnDrawObstacle(mapTM.obstacleData, mapTM.obstacleDataWidth, -mapTM.mapSize / 2, gridUnit);
        }

        void OnDrawGizmos() {
            OnDrawGrid();
            OnDrawObstacle();
        }

    }

}
#endif