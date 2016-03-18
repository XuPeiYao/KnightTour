using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNXConsoleTest{
    public class GridBoard {
        private int[][] Data;
        public int Size {
            get {
                return Data.Length;
            }
        }
        public GridBoard(int Size = 8) {
            Data = new int[Size][];
            for(int i = 0; i < Size; i++) {
                Data[i] = new int[Size];
            }
        }
        public int this[int X, int Y] {
            get {
                return Data[X][Y];
            }
            set {
                Data[X][Y] = value;
            }
        }
        public int this[Point point] {
            get {
                return this[point.X, point.Y];
            }
            set {
                this[point.X, point.Y] = value;
            }
        }
        public bool InBoard(int X,int Y) {//判斷指定座標是否合法
            return X > -1 && X < Size && Y > -1 && Y < Size;
        }
        public bool InBoard(Point point) {
            return InBoard(point.X, point.Y);
        }
        public bool CanMove(int X,int Y) {//判斷指定座標是否尚未使用
            return this[X, Y] == 0;
        }
        public bool CanMove(Point point) {
            return CanMove(point.X, point.Y);
        }
        public GridBoard Clone() {
            GridBoard result = new GridBoard(this.Size);
            for(int i = 0; i < Data.Length; i++) {
                for(int j = 0; j < Data.Length; j++) {
                    result[i, j] = this[i, j];
                }
            }
            return result;
        }
        public override string ToString() {
            return string.Join("\r\n", Data.Select(x => string.Join("", x.Select(y=> string.Format("{0,6:######}",y)))));
        }
    }
}
