

namespace MyLittleRendererV8
{
    public class Vector2
    {
        public float x;
        public float y;

        // Predefined Vectors3  
        #region Default vectors

        private static Vector2 zero = new Vector2(0, 0);
        public static Vector2 Zero
        {
            get => new Vector2(zero.x , zero.y);
        }

        private static Vector2 left = new Vector2(-1, 0);
        public static Vector2 Left
        {
            get => new Vector2(left.x, left.y);
        }

        private static Vector2 right = new Vector2(1, 0);
        public static Vector2 Right
        {
            get => new Vector2(right.x, right.y);
        }

        private static Vector2 up = new Vector2(0, 1);
        public static Vector2 Up
        {
            get => new Vector2(up.x, up.y);
        }

        private static Vector2 down = new Vector2(0, -1);
        public static Vector2 Down
        {
            get => new Vector2(down.x, down.y);
        }

        #endregion 
        // Operations of " + " " - " " * " " / " for Vectors3
        #region Operators
        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            Vector2 v3 = new Vector2(v1.x - v2.x, v1.y - v2.y);
            return v3;
        }
        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            Vector2 v3 = new Vector2(v1.x + v2.x, v1.y + v2.y);
            return v3;
        }
        public static Vector2 operator *(Vector2 v1, float p)
        {
            Vector2 v3 = new Vector2(v1.x * p, v1.y * p);
            return v3;
        }
        public static Vector2 operator /(Vector2 v1, float p)
        {
            Vector2 v3 = new Vector2(v1.x / p, v1.y / p);
            return v3;
        }
        #endregion   

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
