namespace UF_CSharp
{
    class UnionFind
    {
        // 親要素のインデックスを保持する
        // 親要素が存在しない(自身がルートである)とき、マイナスでグループの要素数を持つ
        public int[] Parents { get; set; }
        public UnionFind(int n)
        {
            this.Parents = new int[n];
            for (int i = 0; i < n; i++)
            {
                // 初期状態ではそれぞれが別のグループ(ルートは自分自身)
                // ルートなのでマイナスで要素数(1個)を保持する
                this.Parents[i] = -1;
            }
        }

        // 要素xのルート要素はどれか
        public int Find(int x)
        {
            // 親がマイナスの場合は自分自身がルート
            if (this.Parents[x] < 0) return x;
            // ルートが見つかるまで再帰的に探す
            // 見つかったルートにつなぎかえる
            this.Parents[x] = Find(this.Parents[x]);
            return this.Parents[x];
        }

        // 要素xの属するグループの要素数を取得する
        public int Size(int x)
        {
            // ルート要素を取得して、サイズを取得して返す
            return -this.Parents[this.Find(x)];
        }

        // 要素x, yが同じグループかどうか判定する
        public bool Same(int x, int y)
        {
            return this.Find(x) == this.Find(y);
        }

        // 要素x, yが属するグループを同じグループにまとめる
        public bool Union(int x, int y)
        {
            // x, y のルート
            x = this.Find(x);
            y = this.Find(y);
            // すでに同じグループの場合処理しない
            if (x == y) return false;

            // 要素数が少ないグループを多いほうに書き換えたい
            if (this.Size(x) < this.Size(y))
            {
                var tmp = x;
                x = y;
                y = tmp;
            }
            // まとめる先のグループの要素数を更新
            this.Parents[x] += this.Parents[y];
            // まとめられるグループのルートの親を書き換え
            this.Parents[y] = x;
            return true;
        }
    }
}
