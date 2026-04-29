using Prog2JsonExporter.Scripts.Data;
using UnityEngine;

namespace Prog2JsonExporter.Scripts.SceneData
{
    public class Prog2EnvironmentInfo : MonoBehaviour
    {
        [SerializeField] private Transform camBoundsLeft;
        [SerializeField] private Transform camBoundsRight;
        [SerializeField] private Transform playerSpawnPoint;
        public Prog2EnvironmentSceneInfo GetEnvironmentInfo()
        {
            Prog2EnvironmentSceneInfo scenneInfo = new Prog2EnvironmentSceneInfo();
            scenneInfo.cameraBoundsLeft = transform.position.x + camBoundsLeft.position.x;
            scenneInfo.cameraBoundsRight = transform.position.y + camBoundsRight.position.x;
            scenneInfo.spawnPointX = transform.position.x + playerSpawnPoint.position.x;
            scenneInfo.spawnPointY = transform.position.y + playerSpawnPoint.position.y;
            return scenneInfo;
        }
    }
}