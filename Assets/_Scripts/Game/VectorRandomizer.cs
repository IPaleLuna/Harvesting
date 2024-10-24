using UnityEngine;
namespace PaleLuna.Randomizers
{
    public static class VectorRandomizer
    {
        public static Vector3 RandomPoint(Vector3 area, Vector3 originPoint)
        {
            float randomX = Random.Range(-area.x / 2, area.x / 2);
            float randomY = Random.Range(-area.y / 2, area.y / 2);

            Vector3 randomPoint = new Vector3(randomX, randomY);

            return randomPoint + originPoint;
        }

        public static Vector3 RandomPoint(Vector3 area, Vector3 originPoint, CheckSphere checkSphere)
        {
            Vector3 randomPoint = RandomPoint(area, originPoint);

            if (!checkSphere.TryGetCenterOfObjects(randomPoint, out Vector3 centerOfObjects)) return randomPoint;

            Vector3 newPos = Vector3.zero;
            Vector3 targetDirection = (originPoint - randomPoint).normalized;

            float targetDistance = checkSphere.radius;

            newPos = randomPoint + (targetDirection * targetDistance);

            return newPos;
        }
    }
}

