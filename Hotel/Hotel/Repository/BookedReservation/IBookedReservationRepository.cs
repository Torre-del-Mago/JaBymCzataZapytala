﻿using Hotel.DTO;
using Messages;

namespace Hotel.Repository.BookedReservation
{
    public interface IBookedReservationRepository
    {
        Task<bool> canReservationBeMade(BookedReservationCommand command);

        Task<BookedEvent> insertEvent(BookedReservationCommand command);
    }
}
