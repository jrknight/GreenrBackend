using GreenrAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenrAPI.Services
{
    public interface ITripRepository
    {

        Task<IEnumerable<Trip>> GetTripsAsync();
        Task<Trip> GetTripAsync(int Id);
        void DeleteTrip(int Id);
        Task<bool> TripExistsAsync(int Id);
        Task AddTripAsync(Trip item);
        Task UpdateTripAsync(Trip item);
        Task<bool> SaveAsync();

    }
}
