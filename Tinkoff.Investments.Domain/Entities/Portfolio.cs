using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinkoff.Investments.Domain.Entities
{
    // Портфель инвестора
    public class Portfolio
    {
        private readonly List<Position> _positions = new();
        private Portfolio() { }
        public Portfolio(
            Guid userId,
            string name,
            string currency)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Name = name;
            Currency = currency;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Currency { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public IReadOnlyCollection<Position> Positions => _positions.AsReadOnly();

        // Добавить позицию в портфель
        public void AddPosition(Position position)
        {
            if (position == null) throw new ArgumentNullException(nameof(position));

            _positions.Add(position);
        }
    }
}
