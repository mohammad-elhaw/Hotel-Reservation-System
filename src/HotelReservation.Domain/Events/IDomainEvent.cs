﻿namespace HotelReservation.Domain.Events;
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
