using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Инструмент (акция, облигация, валюта)

namespace Tinkoff.Investments.Domain.Entities
{
    public class Instrument
    {
        private Instrument() { }
        public Instrument(
            string ticker,
            string name,
            string type,
            string currency) 
        {
            Id = Guid.NewGuid();
            Ticker = ticker;
            Name = name;
            Type = type;
            Currency = currency;
            CurrentPrice = 0;
            LastPriceUpdate = DateTime.UtcNow;
        }
        
        public Guid Id { get; private set; }
        public string Ticker { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Currency { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public DateTime LastPriceUpdate { get; private set; }

        // Обновить цену инструмента
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Цена не может быть отрицательной", nameof(newPrice));

            CurrentPrice = newPrice;
            LastPriceUpdate = DateTime.UtcNow;
        }
    }
}
