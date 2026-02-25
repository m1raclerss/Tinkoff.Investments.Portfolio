using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkoff.Investments.Domain.Entities
{
    // Позиция - сколько акций/облигаций куплено в рамках портфеля
    public class Position
    {
        private readonly List<Lot> _lots = new();
        private Position() { }
        public Position(
            Guid instrumentId,
            decimal quantity,
            decimal price)
        {
            Id = Guid.NewGuid();
            InstrumentId = instrumentId;

            var firstLot = new Lot(quantity, price);
            _lots.Add(firstLot);

            Quantity = quantity;
        }

        public Guid Id { get; private set; }
        public Guid InstrumentId { get; private set; }
        public decimal Quantity { get; private set; }
        public IReadOnlyCollection<Lot> Lots => _lots.AsReadOnly();

        // Добавить покупку (увеличить позицию)
        public void AddPurchase(decimal quantity, decimal price) 
        {
            if (quantity < 0) throw new ArgumentOutOfRangeException("Количество должно быть положительным", nameof(quantity));

            var newLot = new Lot(quantity, price);
            _lots.Add(newLot);
            Quantity += quantity;
        }

        // Продать часть позиции (уменьшить позицию)
        public void Sell(decimal quantity) 
        {
            if (quantity <= 0) throw new ArgumentException("Количество должно быть положительным", nameof(quantity));

            if (quantity > Quantity)
                throw new InvalidOperationException($"Продажа {quantity} невозможна, доступно только {Quantity}");

            var remainingToSell = quantity;

            // FIFO
            foreach (var lot in _lots)
            {
                if (remainingToSell <= 0) break;

                var soldFromlot = Math.Min(lot.RemainingQuantity, remainingToSell);
                lot.Reduce(soldFromlot);
                remainingToSell -= soldFromlot;
            }

            Quantity -= quantity;
        }

        // Средняяя цена для покупки
        public decimal GetAveragePurchasePrice()
        {
            decimal totalCost = 0;
            decimal totalQuantity = 0;
            foreach (var lot in _lots)
            {
                totalCost += lot.PurchasePrice * (lot.OriginalQuantity - lot.RemainingQuantity);
                totalQuantity += lot.OriginalQuantity - lot.RemainingQuantity;
            }

            return totalQuantity == 0 ? 0 : totalCost / totalQuantity;
        }

    }
}
