using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkoff.Investments.Domain.Entities
{
    // Лот - одна покупка инструмента
    public class Lot
    {
        private Lot() { }

        public Lot(decimal quantity, decimal price)
        {
            Id = Guid.NewGuid();
            OriginalQuantity = quantity;
            RemainingQuantity = quantity;
            PurchasePrice = price;
            PurchaseDate = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public decimal OriginalQuantity { get; private set; }
        public decimal RemainingQuantity { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public DateTime PurchaseDate { get; private set; }

        // Продать часть лота
        public void Reduce(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Количество должно быть положительным", nameof(quantity));

            if (quantity > RemainingQuantity)
                throw new InvalidOperationException($"Продажа {quantity} невозможна, доступно только {RemainingQuantity}");

            RemainingQuantity -= quantity;
        }

        // Проверка того, продан ли лот полностью
        public bool isFullySold => RemainingQuantity == 0;
    }
}
