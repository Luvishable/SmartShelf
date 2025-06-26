using SmartShelf.Domain.Common;
using SmartShelf.Domain.Enums;

namespace SmartShelf.Domain.Entities;

public class InventoryActionLog
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public Guid? ShelfId { get; private set; }
    public int Quantity { get; private set; }
    public InventoryActionType ActionType { get; private set; }
    public Guid PerformedBy { get; private set; }
    public DateTime Timestamp { get; private set; }

    private InventoryActionLog() { }

    public InventoryActionLog(
        Guid productId,
        Guid? shelfId,
        int quantity,
        InventoryActionType actionType,
        Guid performedBy)
    {
        Guard.AgainstNonPositive(quantity, nameof(quantity));

        ProductId = productId;
        ShelfId = shelfId;
        Quantity = quantity;
        ActionType = actionType;
        PerformedBy = performedBy;
        Timestamp = DateTime.UtcNow;
        Id = Guid.NewGuid();
    }
}
