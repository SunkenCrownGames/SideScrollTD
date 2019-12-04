using AngieTools.V2Tools;
using UnityEngine;
using UnityEngine.Assertions.Must;
// ReSharper disable Unity.PreferNonAllocApi

namespace Level
{
    public class PlatformRayData
    {
        public PlatformRayData(Vector3 p_leftPosition, Vector3 p_leftUpPosition, Vector3 p_leftDownPosition, Vector3 p_rightPosition,
            Vector3 p_rightUpPosition, Vector3 p_rightDownPosition, Vector3 p_upPosition, Vector3 p_downPosition, float p_offsetX, float p_offsetY)
        {
            LeftPosition = p_leftPosition;
            LeftUpPosition = p_leftUpPosition;
            LeftDownPosition = p_leftDownPosition;
            RightPosition = p_rightPosition;
            RightUpPosition = p_rightUpPosition;
            RightDownPosition = p_rightDownPosition;
            UpPosition = p_upPosition;
            DownPosition = p_downPosition;
            OffsetX = p_offsetX;
            OffsetY = p_offsetY;
        }
        
        public PlatformRayData(PlatformRayData p_data)
        {
            LeftPosition = p_data.LeftPosition;
            LeftUpPosition = p_data.LeftUpPosition;
            LeftDownPosition = p_data.LeftDownPosition;
            RightPosition = p_data.RightPosition;
            RightUpPosition = p_data.RightUpPosition;
            RightDownPosition = p_data.RightDownPosition;
            UpPosition = p_data.UpPosition;
            DownPosition = p_data.DownPosition;
            OffsetX = p_data.OffsetX;
            OffsetY = p_data.OffsetY;
        }

        public static void FireRay(PlatformRayData p_data)
        {
            #region  LEFT
            //LEFT
            var inSightLeft = 
                Physics2D.RaycastAll(p_data.LeftPosition, Vector3.left, p_data.OffsetX);
            //LEFT UP
            var inSightUpLeft = 
                Physics2D.RaycastAll(p_data.LeftUpPosition, Vector3.up, p_data.OffsetY);
            //LEFT DOWN
            var inSightDownLeft = 
                Physics2D.RaycastAll(p_data.LeftDownPosition, Vector3.down, p_data.OffsetY);
            //LEFT DIAGONAL DOWN
            var inSightDiagonalDownLeft =
                Physics2D.RaycastAll(p_data.LeftDownPosition, VectorUtils.DiagonalDownLeft, p_data.OffsetY);
            //LEFT DIAGONAL RaycastAll
            var inSightDiagonalUpLeft = 
                Physics2D.RaycastAll(p_data.LeftUpPosition, VectorUtils.DiagonalUpLeft , p_data.OffsetY);
            #endregion

            #region  RIGHT
            //RIGHT
            var inSightRight 
                = Physics2D.RaycastAll(p_data.RightPosition, Vector3.right, p_data.OffsetX);
            //RIGHT UP
            var inSightUpRight 
                = Physics2D.RaycastAll(p_data.RightUpPosition, Vector3.up, p_data.OffsetY);
            //RIGHT DOWN
            var inSightDownRight 
                = Physics2D.RaycastAll(p_data.RightDownPosition, Vector3.down, p_data.OffsetY);
            //LEFT DIAGONAL DOWN
            var inSightDiagonalDownRight 
                = Physics2D.RaycastAll(p_data.RightDownPosition, VectorUtils.DiagonalDownRight , p_data.OffsetY); 
            //LEFT DIAGONAL UP
            var inSightDiagonalUpRight 
                = Physics2D.RaycastAll(p_data.RightUpPosition, VectorUtils.DiagonalUpRight , p_data.OffsetY);
            #endregion
            
            #region MIDDLE
            var inSightUp 
                = Physics2D.RaycastAll(p_data.UpPosition, Vector3.up, p_data.OffsetY);
            var inSightDown 
                = Physics2D.RaycastAll(p_data.DownPosition, Vector3.down, p_data.OffsetY);
            #endregion
            
            Debug.Log($"LEFT: {inSightLeft.Length} LEFT UP: {inSightUpLeft.Length} LEFT DOWN: {inSightDownLeft.Length} LEFT DIAGONAL UP: {inSightDiagonalUpLeft.Length} LEFT DIAGONAL DOWN: {inSightDiagonalDownLeft.Length}");
            Debug.Log($"RIGHT: {inSightRight.Length} RIGHT UP: {inSightUpRight.Length} " +
                      $"RIGHT DOWN: {inSightDownRight.Length} RIGHT DIAGONAL UP: {inSightDiagonalUpRight.Length} RIGHT DIAGONAL DOWN: {inSightDiagonalDownRight.Length}");
        }

        public static void DrawRays(PlatformRayData p_data)
        {
            #region DebugRays
            //LEFT
            Debug.DrawRay(p_data.LeftPosition, Vector3.left * p_data.OffsetX, Color.red, 10f);
            //LEFT UP
            Debug.DrawRay(p_data.LeftUpPosition, Vector3.up * p_data.OffsetY, Color.red, 10f);
            //LEFT DOWN
            Debug.DrawRay(p_data.LeftDownPosition, Vector3.down * p_data.OffsetY, Color.red, 10f);
            //LEFT LOWER DIAGONAL
            Debug.DrawRay(p_data.LeftDownPosition, new Vector3(-1f, -1f).normalized * p_data.OffsetY, Color.red, 10f);
            //LEFT UPPER DIAGONAL
            Debug.DrawRay(p_data.LeftUpPosition, new Vector3(-1f, 1f).normalized * p_data.OffsetY, Color.red, 10f);
            
            //RIGHT
            Debug.DrawRay(p_data.RightPosition, Vector3.right * p_data.OffsetX, Color.red, 10f);
            //RIGHT UP
            Debug.DrawRay(p_data.RightUpPosition, Vector3.up * p_data.OffsetY, Color.red, 10f);
            //RIGHT DOWN
            Debug.DrawRay(p_data.RightDownPosition, Vector3.down * p_data.OffsetY, Color.red, 10f);
            //RIGHT LOWER DIAGONAL
            Debug.DrawRay(p_data.RightDownPosition, new Vector3(1f, -1f).normalized * p_data.OffsetY, Color.red, 10f);
            //RIGHT UPPER DIAGONAL
            Debug.DrawRay(p_data.RightUpPosition, new Vector3(1f, 1f).normalized * p_data.OffsetY, Color.red, 10f);
            
            //MIDDLE UP
            Debug.DrawRay(p_data.UpPosition, Vector3.up * p_data.OffsetY, Color.red, 10f);
            //MIDDLE DOWN
            Debug.DrawRay(p_data.DownPosition, Vector3.down * p_data.OffsetY, Color.red, 10f);

            #endregion

        }

        
        public float OffsetX { get; }
        public float OffsetY { get; }
        
        public Vector3 LeftPosition { get; }

        public Vector3 LeftUpPosition { get; }

        public Vector3 LeftDownPosition { get; }

        public Vector3 RightPosition { get; }

        public Vector3 RightUpPosition { get; }

        public Vector3 RightDownPosition { get; }

        public Vector3 UpPosition { get; }

        public Vector3 DownPosition { get; }
    }
}
