using System.Security.Cryptography;
using System.Text;

namespace blockchain
{
    public class Block<T>
    {
        private string? _hash;
        private int _blockNumber;
        private int randomValue;
        private DateTime time;
        private Block<T?>? block;
        private static DateTime dateTime = DateTime.Now;
        private static Random random = new Random();
        private T? _data;
        private static int staticID = 0;
        public Block(Block<T?> _block, T? value, DateTime timeCreate)
        {
            this.randomValue = random.Next();
            if (dateTime > timeCreate)
            {
                return;
            }
            else
            {
                dateTime = timeCreate;
                time = dateTime;
            }
            _data = value;
            block = _block;
            this._blockNumber = staticID;
            this._hash = this.reHash;
        }
        public Block(T? value, DateTime timeCreate)
        {
            this.time = timeCreate;
            this._data = value;
            this._hash = ComputeHash(value + this._blockNumber.ToString()
                + this.randomValue.ToString() + this.time.ToString());
            block = null;
        }
        private static string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2")); // Chuyển đổi thành chuỗi hex
                }
                return builder.ToString();
            }
        }
        private string? getHash
        {
            get { return _hash; }
        }
        public string reHash
        {
            get
            {
                return ComputeHash(this._data?.ToString() ?? "null" + this._blockNumber.ToString() + this.block?.GetHashCode() ?? "null" + this.randomValue.ToString() + this.time.ToString());
            }
        }
        public T? data
        {
            get
            {
                if (this._data == null) return default(T?); return this._data;
            }
        }
        public bool checkTime(DateTime timeNew)
        {
            if (this.time > timeNew) return false; return true;
        }
        public int blockNumber
        { get { return this._blockNumber; } }
        public bool checkTheVaild
        {
            get
            {
                if (this.block == null) return true;
                return (this.reHash == this._hash || this.checkTheVaild);
            }
        }
    }
}