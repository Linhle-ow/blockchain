
namespace blockchain
{
    internal class Chain<T>
    {
        private Block<T?> block;
        private static int quantity = 0;

        public Chain(T? value, DateTime timeCreate)
        {
            block = new Block<T?>(value, timeCreate); quantity++;
        }

        public Chain(T? value)
        {
            block = new Block<T?>(value, DateTime.Now); quantity++;
        }

        public bool Add(T? value, DateTime timeCreate)
        {
            if (!block.checkTime(timeCreate)) return false;
            quantity++;
            block = new Block<T?>(this.block, value, timeCreate); return true;
        }

        public bool checkTheChainValidity
        {
            get
            {
                if (quantity <= 0) return false;
                if (quantity != this.block.blockNumber) return false;
                return this.block.checkTheVaild;
            }
        }

        public int Quantity
        { get { return quantity; } }

    }
}