using System.Linq;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public enum OrderStatus
    {
        Pending = 0,
        Received = 1,
        InRealization = 2,
        Sent = 3,
        ReadyToPickUp = 4,
        PickedUp = 5
    }
}