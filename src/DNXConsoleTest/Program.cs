using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DNXConsoleTest{
    public class Program {
        public static void Main(string[] args){
            var Board = new GridBoard(Size: 14);//建立棋盤
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var result = KnightTour(Board);
            watch.Stop();
            Console.WriteLine($"運算耗時:{watch.Elapsed}");
            Console.WriteLine(result.ToString());
        }

        public static Point[] Steps = {//可移動座標
            new Point() { X = -2 , Y = -1},
            new Point() { X = -1 , Y = -2},
            new Point() { X =  2 , Y = -1},
            new Point() { X =  1 , Y = -2},
            new Point() { X =  2 , Y =  1},
            new Point() { X =  1 , Y =  2},
            new Point() { X = -2 , Y =  1},
            new Point() { X = -1 , Y =  2},
        };
        public static GridBoard KnightTour(GridBoard Board) => KnightTour(Board, new Point());
        public static GridBoard KnightTour(GridBoard Board, Point Start ,int Step = 0) {
            Board[Start] = ++Step;//將目前座標填入值

            if (Step == Board.Size * Board.Size)return Board;//已經完成巡邏

            foreach(var NextPoint in Steps//列舉可移動座標並且帶入任務檢驗
                .Select(x=>x.Move(Start))//將移動後的座標列出
                .Where(x=>Board.InBoard(x) && Board.CanMove(x))//篩選出可以使用的座標(剔除超出邊界與已經使用過的)
                .Select(x => {
                    var NextBoard = Board.Clone();//複製目前棋盤狀態
                    NextBoard[x] = Step + 1;//在移動後的位置填寫新的step值

                    var NextNextMove = (from t in Steps select t.Move(x)).Where(y=>NextBoard.InBoard(y) && NextBoard.CanMove(y));
                    //計算下一節點可移動的路徑集合

                    return new {//建立匿名物件
                        Point = x,//下一階層移動座標
                        Degree = NextNextMove.Count()//計算下一節點可移動的路徑數量
                    };
                })
                .OrderBy(x=>x.Degree)//使用可移動數量進行遞增排序
            ) {
                var NextLevelResult = KnightTour(Board, NextPoint.Point, Step);//使用遞迴並指派下個移動位置
                if (NextLevelResult != null) return NextLevelResult;//如果下一層有回傳結果則代表是可行路徑則回傳下一層結果
            }

            Board[Start] = 0;//如果上面迴圈沒有回傳則代表本層是失敗的，清除本層對盤面的影響
            return null;//回傳結果為空，因為下一步都無法走而為空值
        }
    }
}
