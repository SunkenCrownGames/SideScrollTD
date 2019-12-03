using UnityEngine;

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

        public static void DrawRays(PlatformRayData p_data)
        {
            #region DebugRays
            //LEFT
            Debug.DrawRay(p_data.LeftPosition, Vector3.left * p_data.OffsetX, Color.blue, 10f);
            //LEFT UP
            Debug.DrawRay(p_data.LeftUpPosition, Vector3.up * p_data.OffsetY, Color.blue, 10f);
            //LEFT DOWN
            Debug.DrawRay(p_data.LeftDownPosition, Vector3.down * p_data.OffsetY, Color.blue, 10f);
            //LEFT LOWER DIAGONAL
            Debug.DrawRay(p_data.LeftDownPosition, new Vector3(-1f, -1f) * p_data.OffsetY, Color.blue, 10f);
            //LEFT UPPER DIAGONAL
            Debug.DrawRay(p_data.LeftUpPosition, new Vector3(-1f, 1f) * p_data.OffsetY, Color.blue, 10f);
            
            //RIGHT
            Debug.DrawRay(p_data.RightPosition, Vector3.right * p_data.OffsetX, Color.blue, 10f);
            //RIGHT UP
            Debug.DrawRay(p_data.RightUpPosition, Vector3.up * p_data.OffsetY, Color.blue, 10f);
            //RIGHT DOWN
            Debug.DrawRay(p_data.RightDownPosition, Vector3.down * p_data.OffsetY, Color.blue, 10f);
            //RIGHT LOWER DIAGONAL
            Debug.DrawRay(p_data.RightDownPosition, new Vector3(1f, -1f) * p_data.OffsetY, Color.blue, 10f);
            //RIGHT UPPER DIAGONAL
            Debug.DrawRay(p_data.RightUpPosition, new Vector3(1f, 1f) * p_data.OffsetY, Color.blue, 10f);
            
            //MIDDLE UP
            Debug.DrawRay(p_data.UpPosition, Vector3.up * p_data.OffsetY, Color.blue, 10f);
            //MIDDLE DOWN
            Debug.DrawRay(p_data.DownPosition, Vector3.down * p_data.OffsetY, Color.blue, 10f);

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
