using System;

namespace Ecommerce
{
    public class Item
    {
        public Item(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}